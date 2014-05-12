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
        private List<ProjectLineViewModel> m_ProjectLineViewModelList;

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

        public ProjectLineController()
        {
            m_ProjectLineRepository = new ProjectLineRepository();
            m_ProjectRepository = new ProjectRepository();
            m_ProjectLineViewModelList = new List<ProjectLineViewModel>();
        }

        // GET: /ProjectLine/
        public ActionResult Index()
        {
            foreach (ProjectLine x in m_ProjectLineRepository.GetAll().ToList())
            {
                ProjectLineViewModel projectLineViewModel = new ProjectLineViewModel(x);
                m_ProjectLineViewModelList.Add(projectLineViewModel);
            }

            return View(m_ProjectLineViewModelList);            
        }

        // GET: /ProjectLine/Details/5
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

        // GET: /ProjectLine/Create
        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(m_ProjectRepository.GetAll(), "Id", "Name");
            return View();
        }

        // POST: /ProjectLine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: /ProjectLine/Edit/5
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

        // POST: /ProjectLine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: /ProjectLine/Delete/5
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

        // POST: /ProjectLine/Delete/5
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
