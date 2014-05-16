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
    public class ProjectLineController : Controller
    {
        private IProjectLineRepository m_ProjectLineRepository;
        private IProjectRepository m_ProjectRepository;

        public ProjectLineController()
        {
            m_ProjectLineRepository = new ProjectLineRepository();
            m_ProjectRepository = new ProjectRepository();
        }

        public ProjectLineController(IProjectLineRepository repository)
        {
            // only used by unit testing
            m_ProjectLineRepository = repository;
            m_ProjectRepository = new ProjectRepository();
        }


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

        public ActionResult Index()
        {
            List<ProjectLineViewModel> m_ProjectLineViewModelList = new List<ProjectLineViewModel>();

            foreach (ProjectLine x in m_ProjectLineRepository.GetAll().ToList())
            {
                ProjectLineViewModel projectLineViewModel = new ProjectLineViewModel(x);
                m_ProjectLineViewModelList.Add(projectLineViewModel);
            }

            return View(m_ProjectLineViewModelList);            
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
 
            ProjectLine projectLine = m_ProjectLineRepository.GetSingle(id);

            if (projectLine == null)
            {
                return HttpNotFound();
            }
            return View(new ProjectLineViewModel(projectLine));
        }

        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(m_ProjectRepository.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,ProjectId,User,TimeFrom,TimeTo,TextLine1,TextLine2,Date,Language")] ProjectLineViewModel projectLineViewModel)
        {
            ProjectLine projectLine = new ProjectLine();
            projectLine = projectLineViewModel.CastViewModelToModel();
            projectLine.User = GetUsername();
            projectLine.Date = DateTime.Now;

            if (ModelState.IsValid)
            {
                m_ProjectLineRepository.Create(projectLine);
                m_ProjectLineRepository.Save();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(m_ProjectRepository.GetAll(), "Id", "Name", projectLineViewModel.ProjectId);
            return View(projectLineViewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProjectLine projectLine = m_ProjectLineRepository.GetSingle(id);
            if (projectLine == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProjectId = new SelectList(m_ProjectRepository.GetAll(), "Id", "Name", projectLine.ProjectId);
            return View(new ProjectLineViewModel(projectLine));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,ProjectId,User,TimeFrom,TimeTo,TextLine1,TextLine2,Date,Language")] ProjectLineViewModel projectLineViewModel)
        {
            if (ModelState.IsValid)
            {
                ProjectLine projectLine = new ProjectLine();
                projectLine = projectLineViewModel.CastViewModelToModel();
                projectLine.User = GetUsername();
                projectLine.Date = DateTime.Now;
                m_ProjectLineRepository.Update(projectLine);
                m_ProjectRepository.Save();
                return RedirectToAction("Index");               
            }

            ViewBag.ProjectId = new SelectList(m_ProjectRepository.GetAll(), "Id", "Name", projectLineViewModel.ProjectId);
            return View(projectLineViewModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProjectLine projectLine = m_ProjectLineRepository.GetSingle(id);
            if (projectLine == null)
            {
                return HttpNotFound();
            }

            return View(new ProjectLineViewModel (projectLine));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            ProjectLine projectLine = m_ProjectLineRepository.GetSingle(id);
            if (projectLine != null)
            {
                m_ProjectLineRepository.Delete(id);
                m_ProjectLineRepository.Save();
            }

            else
            {
                return HttpNotFound();
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            m_ProjectLineRepository.Dispose();
            m_ProjectRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}
