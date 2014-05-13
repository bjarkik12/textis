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

        public ActionResult Index(string searchString)
        {
            var project = from m in m_ProjectRepository.GetAll()
                          select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                project = project.Where(s => s.Name.Contains(searchString));
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
            return View(projectViewModel);
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file, int id)
        {
            ProjectLineRepository m_ProjectLineRepository = new ProjectLineRepository();
            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                // extract only the fielname
                var fileName = Path.GetFileName(file.FileName);
                //var x = Path.GetExtension(file.FileName); Check if file is .srt ???
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/App_Data"), fileName);
                file.SaveAs(path);

                StreamReader streamUpload = new StreamReader(path);
                string fileLine = "";
                string timeCapsule;
                string fakeYear = "1/1/2000 ";
                string fakeDot = ".";
  
                while (fileLine != null)
                {

                    ProjectLine line = new ProjectLine();

                    fileLine = streamUpload.ReadLine(); //This is the line number which we will not use
                    fileLine = streamUpload.ReadLine(); //now myLine is holding the time
                    //Now we need to get the timestring to the correct format before parsing it to DateTime
                    timeCapsule = fakeYear + fileLine.Substring(0, 8) + fakeDot + fileLine.Substring(9, 3);
                    line.TimeFrom = Convert.ToDateTime(timeCapsule);
                    timeCapsule = fakeYear + fileLine.Substring(17, 8) + fakeDot + fileLine.Substring(26, 3);
                    line.TimeTo = Convert.ToDateTime(timeCapsule);

                    //we are sure to have at least one line
                    fileLine = streamUpload.ReadLine();
                    line.TextLine1 = fileLine;

                    //there may or may not be a second line
                    fileLine = streamUpload.ReadLine();

                    if (fileLine != "" && fileLine != null)
                    {
                        line.TextLine2 = fileLine;
                        fileLine = streamUpload.ReadLine();
                    }
                    //just in case there are more lines that we cannot handle
                    while (fileLine != "" && fileLine != null)
                    {
                        fileLine = streamUpload.ReadLine();
                    }

                    line.Date = DateTime.Now;
                    line.User = GetUsername();
                    line.Language = "EN";
                    line.ProjectId = id;

                    //The parallel Icelandic text:
                    ProjectLine lineIcelandic = new ProjectLine();
                    lineIcelandic.ProjectId = line.ProjectId;
                    lineIcelandic.Language = "IS";
                    lineIcelandic.TimeFrom = line.TimeFrom;
                    lineIcelandic.TimeTo = line.TimeTo;

                    m_ProjectLineRepository.Create(line);
                    m_ProjectLineRepository.Create(lineIcelandic);
                }


                streamUpload.Close();
                m_ProjectLineRepository.Save();
                System.IO.File.Delete(path);
            }

            // We should not redirect to Index - rather to Edit/?  - but how do we get the correct ID ?
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
