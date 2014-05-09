﻿using System;
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
    public class CommentController : Controller
    {
        //private TextisModelContainer db = new TextisModelContainer();

        ICommentRepository m_DataBase;
        //TextisModelContainer db;
        private TextisModelContainer db;


        public CommentController()
        {
            m_DataBase = new CommentRepository();
            db = new TextisModelContainer();
        }


        // GET: /Comment/
        public ActionResult Index()
        {
            //var comment = db.Comment.Include(c => c.Project);
            //return View(comment.ToList());
            var comment = m_DataBase.GetAll();
            return View(comment.ToList());
        }

        // GET: /Comment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Comment comment = db.Comment.Find(id);
            Comment comment = m_DataBase.GetSingle(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: /Comment/Create
        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(db.Project, "Id", "Name");
            return View();
        }

        // POST: /Comment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,ProjectId,Text,User,Date")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                //db.Comment.Add(comment);
                //db.SaveChanges();
                m_DataBase.Create(comment);

                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(db.Project, "Id", "Name", comment.ProjectId);
            return View(comment);
        }

        // GET: /Comment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Comment comment = db.Comment.Find(id);
            Comment comment = m_DataBase.GetSingle(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Project, "Id", "Name", comment.ProjectId);
            return View(comment);
        }

        // POST: /Comment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,ProjectId,Text,User,Date")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(comment).State = EntityState.Modified;
                //db.SaveChanges();
                m_DataBase.Update(comment);
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Project, "Id", "Name", comment.ProjectId);
            return View(comment);
        }

        // GET: /Comment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Comment comment = db.Comment.Find(id);
            Comment comment = m_DataBase.GetSingle(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: /Comment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Comment comment = db.Comment.Find(id);
            //db.Comment.Remove(comment);
            //db.SaveChanges();
            Comment comment = m_DataBase.GetSingle(id);
            if (comment != null)
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