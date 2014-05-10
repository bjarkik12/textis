using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
        //private TextisModelContainer db = new TextisModelContainer();

        private IProjectRepository m_ProjectRepository;
        private ICategoryRepository m_CategoryRepository;
        private ProjectViewModel m_ProjectViewModel;
        //TextisModelContainer db;
        //private TextisModelContainer db;

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
            //db = new TextisModelContainer();
        }

        // GET: /Project/


        public ActionResult Index(string searchString)
        {
            var project = from m in m_ProjectRepository.GetAll()
                          select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                project = project.Where(s => s.Name.Contains(searchString));
            }

            return View(project.ToList());
        }
        // public ActionResult Index()
       // {
            
            
            //var project = db.Project.Include(p => p.Category);
            //var project = m_ProjectRepository.GetAll();
         //   return View(project.ToList());
       // }

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
            return View(project);
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
        public ActionResult Create([Bind(Include="Id,User,Date,Name,Status,Url,CategoryId")] Project project)
        {
            project.User = GetUsername();
            project.Date = DateTime.Now;
            
            if (ModelState.IsValid)
            {
                m_ProjectRepository.Create(project);
                m_ProjectRepository.Save();
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(m_CategoryRepository.GetAll(), "Id", "Name", project.CategoryId);
            return View(project);
        }

        // GET: /Project/Create
        public ActionResult Create2()
        {
            ViewBag.CategoryId = new SelectList(m_CategoryRepository.GetAll(), "Id", "Name");
            return View();
        }
        
        // POST: /Project/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create2([Bind(Include = "Id,User,Date,Name,Status,Url,CategoryId")] ProjectViewModel projectViewModel)
        {
            //Project m_ProjectCast = projectViewModel.CastViewModelToModel(m_ProjectCast);
            Project project = new Project();

            if (ModelState.IsValid)
            {
                project = projectViewModel.CastViewModelToModel();
                m_ProjectRepository.Create(project);
                m_ProjectRepository.Save();
                //db.SaveChanges();
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
            //Project project = db.Project.Find(id);
            Project project = m_ProjectRepository.GetSingle(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(m_CategoryRepository.GetAll(), "Id", "Name", project.CategoryId);
            return View(project);
        }

        public ActionResult Edit2(int? id)
        {

            m_ProjectViewModel = new ProjectViewModel(m_ProjectRepository.GetSingle(id));

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Project project = db.Project.Find(id);
            Project project = m_ProjectRepository.GetSingle(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(m_CategoryRepository.GetAll(), "Id", "Name", project.CategoryId);
            
            return View(m_ProjectViewModel);
        }


        // POST: /Project/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,User,Date,Name,Status,Url,CategoryId")] Project project)
        {
            project.User = GetUsername();
            project.Date = DateTime.Now;

            if (ModelState.IsValid)
            {
                //db.Entry(project).State = EntityState.Modified;
                m_ProjectRepository.Update(project);
                m_ProjectRepository.Save();
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(m_CategoryRepository.GetAll(), "Id", "Name", project.CategoryId);
            return View(project);
        }

        // GET: /Project/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Project project = db.Project.Find(id);
            Project project = m_ProjectRepository.GetSingle(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: /Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Project project = db.Project.Find(id);
            //db.Project.Remove(project);
            //db.SaveChanges();
            Project project = m_ProjectRepository.GetSingle(id);
            if (project != null)
            {
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
            //m_ProjectRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}
