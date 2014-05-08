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
    public class ProjectController : Controller
    {
        //private TextisModelContainer db = new TextisModelContainer();

        IProjectRepository m_DataBase;
        TextisModelContainer db;

        public ProjectController()
        {
            m_DataBase = new ProjectRepository();
            db = new TextisModelContainer();
        }

        // GET: /Project/
        public ActionResult Index()
        {
            //var project = db.Project.Include(p => p.Category);
            var project = m_DataBase.GetAll();
            return View(project.ToList());
        }

        // GET: /Project/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = m_DataBase.GetSingle(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: /Project/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name");
            return View();
        }

        // POST: /Project/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,User,Date,Name,Status,Url,CategoryId")] Project project)
        {
            if (ModelState.IsValid)
            {
                m_DataBase.Create(project);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name", project.CategoryId);
            return View(project);
        }

        // GET: /Project/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Project project = db.Project.Find(id);
            Project project = m_DataBase.GetSingle(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name", project.CategoryId);
            return View(project);
        }

        // POST: /Project/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,User,Date,Name,Status,Url,CategoryId")] Project project)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(project).State = EntityState.Modified;
                m_DataBase.Update(project);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name", project.CategoryId);
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
            Project project = m_DataBase.GetSingle(id);
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
            m_DataBase.Delete(id);
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
