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

namespace textis.Controllers
{
    public class ProjectLineController : Controller
    {
       // private TextisModelContainer db = new TextisModelContainer();
        IProjectLineRepository m_ProjectLineRepository;
        IProjectRepository m_ProjectRepository;
        //TextisModelContainer db;
        //private TextisModelContainer db;


        public ProjectLineController()
        {
            m_ProjectLineRepository = new ProjectLineRepository();
            m_ProjectRepository = new ProjectRepository();
            //db = new TextisModelContainer();
        }
        // GET: /ProjectLine/
        public ActionResult Index()
        {
           // var projectline = db.ProjectLine.Include(p => p.Project);
            //return View(projectline.ToList());
            var projectLine = m_ProjectLineRepository.GetAll();
            return View(projectLine.ToList());
        }

        // GET: /ProjectLine/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // ProjectLine projectline = db.ProjectLine.Find(id);
            ProjectLine projectLine = m_ProjectLineRepository.GetSingle(id);

            if (projectLine == null)
            {
                return HttpNotFound();
            }
            return View(projectLine);
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
        public ActionResult Create([Bind(Include="Id,ProjectId,User,TimeFrom,TimeTo,TextLine1,TextLine2,Date,Language")] ProjectLine projectLine)
        {
            if (ModelState.IsValid)
            {
                //db.ProjectLine.Add(projectline);
                //db.SaveChanges();
                m_ProjectLineRepository.Create(projectLine);
                m_ProjectLineRepository.Save();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(m_ProjectRepository.GetAll(), "Id", "Name", projectLine.ProjectId);
            return View(projectLine);
        }

        // GET: /ProjectLine/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //ProjectLine projectline = db.ProjectLine.Find(id);
            ProjectLine projectLine = m_ProjectLineRepository.GetSingle(id);
            if (projectLine == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(m_ProjectRepository.GetAll(), "Id", "Name", projectLine.ProjectId);
            return View(projectLine);
        }

        // POST: /ProjectLine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,ProjectId,User,TimeFrom,TimeTo,TextLine1,TextLine2,Date,Language")] ProjectLine projectLine)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(projectline).State = EntityState.Modified;
                //db.SaveChanges();
                m_ProjectLineRepository.Update(projectLine);
                m_ProjectLineRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(m_ProjectRepository.GetAll(), "Id", "Name", projectLine.ProjectId);
            return View(projectLine);
        }

        // GET: /ProjectLine/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // ProjectLine projectLine = db.ProjectLine.Find(id);
            ProjectLine projectLine = m_ProjectLineRepository.GetSingle(id);
            if (projectLine == null)
            {
                return HttpNotFound();
            }
            return View(projectLine);
        }

        // POST: /ProjectLine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //ProjectLine projectline = db.ProjectLine.Find(id);
            //db.ProjectLine.Remove(projectline);
            //db.SaveChanges();
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
