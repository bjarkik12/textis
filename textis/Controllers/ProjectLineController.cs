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
        IProjectLineRepository m_DataBase;
        //TextisModelContainer db;
        private TextisModelContainer db;


        public ProjectLineController()
        {
            m_DataBase = new ProjectLineRepository();
            db = new TextisModelContainer();
        }
        // GET: /ProjectLine/
        public ActionResult Index()
        {
           // var projectline = db.ProjectLine.Include(p => p.Project);
            //return View(projectline.ToList());
            var projectLine = m_DataBase.GetAll();
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
            ProjectLine projectLine = m_DataBase.GetSingle(id);

            if (projectLine == null)
            {
                return HttpNotFound();
            }
            return View(projectLine);
        }

        // GET: /ProjectLine/Create
        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(db.Project, "Id", "Name");
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
                m_DataBase.Create(projectLine);
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(db.Project, "Id", "Name", projectLine.ProjectId);
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
            ProjectLine projectLine = m_DataBase.GetSingle(id);
            if (projectLine == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Project, "Id", "Name", projectLine.ProjectId);
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
                m_DataBase.Update(projectLine);
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Project, "Id", "Name", projectLine.ProjectId);
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
            ProjectLine projectLine = m_DataBase.GetSingle(id);
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
            ProjectLine projectLine = m_DataBase.GetSingle(id);
            if (projectLine != null)
            {
                m_DataBase.Delete(id);
            }
            else
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
