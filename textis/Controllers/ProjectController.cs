using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using textis;
using textis.Repository;
using textis.ViewModel;

namespace textis.Controllers
{
    public class ProjectController : Controller
    {
        private IProjectRepository m_ProjectRepository;
        private ICategoryRepository m_CategoryRepository;
        private ICommentRepository m_CommentRepository;
        private IUpvoteRepository m_UpvoteRepository;
        private IProjectLineRepository m_ProjectLineRepository;
        private ProjectViewModel m_ProjectViewModel;
        private List<ProjectViewModel> m_ProjectViewModelList;

        public string GetUsername()
        {
            if (Request.IsAuthenticated)
            {
                return User.Identity.Name;
            }
            else
            {
                return "Nafnlaus";
            }
        }

        public ProjectController()
        {
            m_ProjectRepository = new ProjectRepository();
            m_CategoryRepository = new CategoryRepository();
            m_ProjectViewModel = new ProjectViewModel();
            m_ProjectViewModelList = new List<ProjectViewModel>();
            m_CommentRepository = new CommentRepository();
            m_UpvoteRepository = new UpvoteRepository();
            m_ProjectLineRepository = new ProjectLineRepository();
        }

        public ActionResult Index(string category, string searchString, string sortOrder)
        {
            var categoryList = new List<string>();

            var categoryQuery = from n in m_ProjectRepository.GetAll()
                                orderby n.Category.Name
                                select n.Category.Name;

            categoryList.AddRange(categoryQuery.Distinct());
            ViewBag.category = new SelectList(categoryList);

            var project = from m in m_ProjectRepository.GetAll()
                          select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                project = project.Where(s => s.Name.ToLower().Contains(searchString.ToLower()));
            }

            if (!string.IsNullOrEmpty(category))
            {
                project = project.Where(x => x.Category.Name == category);
            }

            //tilraun          
            ViewBag.userSort = String.IsNullOrEmpty(sortOrder) ? "User" : "";
            ViewBag.dateSort = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.nameSort = String.IsNullOrEmpty(sortOrder) ? "Name" : "";
            ViewBag.statusSort = String.IsNullOrEmpty(sortOrder) ? "Status" : "";
            ViewBag.categorySort = String.IsNullOrEmpty(sortOrder) ? "Category" : "";

            switch (sortOrder)
            {
                case "User":
                    project = project.OrderBy(s => s.User);
                    break;
                //case "user_des":
                //    project = project.OrderByDescending(s => s.User);
                //    break;
                case "date_desc":
                    project = project.OrderByDescending(s => s.Date);
                    break;
                case "Date":
                    project = project.OrderBy(s => s.Date);
                    break;
                case "Name":
                    project = project.OrderBy(s => s.Name);
                    break;
                case "name_des":
                    project = project.OrderByDescending(s => s.Name);
                    break;
                case "Status":
                    project = project.OrderBy(s => s.Status);
                    break;
                case "Category":
                    project = project.OrderBy(s => s.Category.Name);
                    break;
                default:
                    project = project.OrderBy(s => s.Name);
                    break;
            }

            foreach (Project x in project.ToList())
            {
                ProjectViewModel projectViewModel = new ProjectViewModel(x);
                m_ProjectViewModelList.Add(projectViewModel);
            }

            return View(m_ProjectViewModelList);
        }

        // GET: /Project/Details/5
        
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = m_ProjectRepository.GetSingle(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            return View(new ProjectViewModel (project));
        }

        // GET: /Project/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(m_CategoryRepository.GetAll(), "Id", "Name");
            return View();
        }
        
        // POST: /Project/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,User,Date,Name,Status,Url,CategoryId")] ProjectViewModel projectViewModel)
        {
            if (ModelState.IsValid)
            {
                Project project = new Project();
                projectViewModel.User = GetUsername();
                projectViewModel.Date = DateTime.Now;
                projectViewModel.Status = "Stofnað";
                project = projectViewModel.CastViewModelToModel();
                m_ProjectRepository.Create(project);
                m_ProjectRepository.Save();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(m_CategoryRepository.GetAll(), "Id", "Name", projectViewModel.CategoryId);
            return View(projectViewModel);
        }

        // GET: /Project/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Project project = m_ProjectRepository.GetSingle(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            m_ProjectViewModel = new ProjectViewModel(m_ProjectRepository.GetSingle(id));
            ViewBag.CategoryId = new SelectList(m_CategoryRepository.GetAll(), "Id", "Name", project.CategoryId);
            return View(m_ProjectViewModel);
        }

        // POST: /Project/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,User,Date,Name,Status,Url,CategoryId")] ProjectViewModel projectViewModel)
        {
            if (ModelState.IsValid)
            {
                Project project = new Project();
                project = projectViewModel.CastViewModelToModel();
                project.User = GetUsername();
                project.Date = DateTime.Now;
                m_ProjectRepository.Update(project);
                m_ProjectRepository.Save();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(m_CategoryRepository.GetAll(), "Id", "Name", projectViewModel.CategoryId);

            //string[] arrStatusList = { "Stofnað", "Í vinnslu", "Tilbúið" };
            //ViewBag.StatusList = new SelectList(arrStatusList, "Status", projectViewModel.Status);

            return View(projectViewModel);
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file, int id)
        {
            // Todo:
            // Allow only .srt files
            // Error handling: try/cacth for corrupt or wrong files
            //ProjectLineRepository m_ProjectLineRepository = new ProjectLineRepository();
            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                //Get rid of previous text lines, if there are any
                var projectToUpload = from x in m_ProjectLineRepository.GetByProjectId(id)
                                      select x;

                if (projectToUpload != null)
                {
                    foreach (ProjectLine x in projectToUpload)
                    {
                        m_ProjectLineRepository.Delete(x.Id);
                    }
                }

                // extract only the fielname
                var fileName = Path.GetFileName(file.FileName);
                //var x = Path.GetExtension(file.FileName); Check if file is .srt ???
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/App_Data"), fileName);
                file.SaveAs(path);

                //Various vaiables
                StreamReader streamUpload = new StreamReader(path);
                string fileLine = "";
                string timeCapsule;
                string fakeYear = "1/1/2000 ";
                string fakeDot = ".";
                DateTime fakeNow = DateTime.Now;
                string fakeUser = GetUsername();

                //to gain speed the file is stored in an array and the stream is closed
                string[] linesOfUpload = System.IO.File.ReadAllLines(path);
                int numberOfLines = linesOfUpload.Length;
                streamUpload.Close();
                System.IO.File.Delete(path);

                //This is working too slow but WHY !!!???
                //Need to clean up -> too many if (break)
                for(int i = 0; i < numberOfLines; )
                {
                    fileLine = linesOfUpload[i++]; //This is the line number (which we will not use)

                    //in case the file has extra empty lines
                    if (fileLine == "" || i >= numberOfLines)
                    {
                        while (fileLine == "")
                        {
                            if (i >= numberOfLines)
                            {
                                break;
                            }
                            fileLine = linesOfUpload[i++];   
                        }
                    }

                    ProjectLine line = new ProjectLine();

                    fileLine = linesOfUpload[i++]; //now myLine is holding the time
                    //Now we need to get the timestring to the correct format before parsing it to DateTime
                    timeCapsule = fakeYear + fileLine.Substring(0, 8) + fakeDot + fileLine.Substring(9, 3);
                    line.TimeFrom = Convert.ToDateTime(timeCapsule);
                    timeCapsule = fakeYear + fileLine.Substring(17, 8) + fakeDot + fileLine.Substring(26, 3);
                    line.TimeTo = Convert.ToDateTime(timeCapsule);

                    //we are sure to have at least one line
                    fileLine = linesOfUpload[i++];
                    line.TextLine1 = fileLine;

                    //in case it was the last line
                    if (i >= numberOfLines)
                    {
                        break;
                    }

                    //there may or may not be a second line
                    fileLine = linesOfUpload[i++];

                    if (fileLine != "")
                    {
                        line.TextLine2 = fileLine;
                        fileLine = linesOfUpload[i++];
                    }

                    //again in case it was the last line
                    if (i >= numberOfLines)
                    {
                        break;
                    }

                    //just in case there are more lines that we cannot handle
                    while (fileLine != "" && i < numberOfLines)
                    {
                        fileLine = linesOfUpload[i++];

                        if (i >= numberOfLines)
                        {
                            break;
                        }
                    }

                    line.Date = fakeNow;
                    line.User = fakeUser;
                    line.Language = "EN";
                    line.ProjectId = id;

                    //The parallel Icelandic text:
                    ProjectLine lineIcelandic = new ProjectLine();
                    lineIcelandic.ProjectId = line.ProjectId;
                    lineIcelandic.Language = "IS";
                    lineIcelandic.TimeFrom = line.TimeFrom;
                    lineIcelandic.TimeTo = line.TimeTo;
                    lineIcelandic.Date = fakeNow;
                    lineIcelandic.User = fakeUser;

                    m_ProjectLineRepository.Create(line);
                    m_ProjectLineRepository.Create(lineIcelandic);
                }

                m_ProjectLineRepository.Save();
            }

            return RedirectToAction("Edit", new { id = id });
        }

        [HttpPost]
        public ActionResult DownloadFile(int? id)
        {
            //var projectToDownload = m_ProjectLineRepository.GetByProjectId(id);
            //projectToDownload = projectToDownload.Where(x => x.Language == "IS");
            var projectToDownload = from x in m_ProjectLineRepository.GetByProjectId(id)
                                    where x.Language == "IS"
                                    orderby x.TimeFrom ascending
                                    select x;
               
           // ProjectLine line = new ProjectLine();

            int i = 0; // array locaton
            int j = 1; //Line numbers to be printed
            string time;
            //all print lines collected in a array
            string[] linesToPrint = new string[10000];

            foreach(ProjectLine line in projectToDownload)
            {
                linesToPrint[i++] = j.ToString(); //The number line

                //Time to-from line
                time = line.TimeFrom.ToString("HH:mm:ss,fff") + " --> ";
                time = time + line.TimeFrom.ToString("HH:mm:ss,fff");
                linesToPrint[i++] = time;

                //first text line
                linesToPrint[i++] = line.TextLine1;

                if (line.TextLine2 != null)
                {   
                    //second text line
                    linesToPrint[i++] = line.TextLine2;
                }

                linesToPrint[i++] = "";

                j++;
            }

            //create and store a file
            var project = m_ProjectRepository.GetSingle(id);
            string fileName = project.Name + ".srt";
            var path = Path.Combine(Server.MapPath("~/App_Data"), fileName);
            System.IO.File.WriteAllLines(path, linesToPrint);

            //send the new file to the user
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("content-disposition", "attachment;filename=" + fileName);
            Response.TransmitFile(path);
            Response.End();
            System.IO.File.Delete(path);

            return RedirectToAction("Edit", new { id = id });
        }

        // GET: /Project/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = m_ProjectRepository.GetSingle(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(new ProjectViewModel (project));
        }

        // POST: /Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = m_ProjectRepository.GetSingle(id);
            if (project != null)
            {
                IEnumerable<Comment> m_CommentQuery = new List<Comment>();
                m_CommentQuery = (from x in m_CommentRepository.GetAll()
                              where x.ProjectId == id
                              select x);

                foreach (Comment comment in m_CommentQuery)
                {
                   m_CommentRepository.Delete(comment.Id);
                }

                m_CommentRepository.Save();

                IEnumerable<Upvote> m_UpvoteQuery = new List<Upvote>();
                m_UpvoteQuery = (from x in m_UpvoteRepository.GetAll()
                                  where x.ProjectId == id
                                  select x);

                foreach (Upvote upvote in m_UpvoteQuery)
                {
                    if (upvote != null)
                    {
                        m_UpvoteRepository.Delete(upvote.Id);
                    }
                }

                m_UpvoteRepository.Save();

                IEnumerable<ProjectLine> m_ProjectLineQuery = new List<ProjectLine>();
                m_ProjectLineQuery = (from x in m_ProjectLineRepository.GetAll()
                                 where x.ProjectId == id
                                 select x);

                foreach (ProjectLine projectLine in m_ProjectLineQuery)
                {
                    m_ProjectLineRepository.Delete(projectLine.Id);
                }

                m_ProjectLineRepository.Save();
                m_ProjectRepository.Delete(id);
                m_ProjectRepository.Save();
            }
            else
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            m_ProjectRepository.Dispose();
            m_CategoryRepository.Dispose();
            m_CommentRepository.Dispose();
            m_UpvoteRepository.Dispose();
            m_ProjectLineRepository.Dispose();
            base.Dispose(disposing);
        }
        
    }
}
