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
    public class UpvoteController : Controller
    {
        //private TextisModelContainer db = new TextisModelContainer();
        IUpvoteRepository m_DataBase;
        //TextisModelContainer db;
        private TextisModelContainer db;


        public UpvoteController()
        {
            m_DataBase = new UpvoteRepository();
            db = new TextisModelContainer();
        }

        // GET: /Upvote/
        public ActionResult Index()
        {
           // var upvote = db.Upvote.Include(u => u.Project);
           // return View(upvote.ToList());
            var comment = m_DataBase.GetAll();
            return View(comment.ToList());
        }

        // GET: /Upvote/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Upvote upvote = db.Upvote.Find(id);
            Upvote upvote = m_DataBase.GetSingle(id);
            if (upvote == null)
            {
                return HttpNotFound();
            }
            return View(upvote);
        }

        // GET: /Upvote/Create
        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(db.Project, "Id", "Name");
            return View();
        }

        // POST: /Upvote/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,ProjectId,User,Date")] Upvote upvote)
        {
            if (ModelState.IsValid)
            {
                //db.Upvote.Add(upvote);
                //db.SaveChanges();
                m_DataBase.Create(upvote);
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(db.Project, "Id", "Name", upvote.ProjectId);
            return View(upvote);
        }

        // GET: /Upvote/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Upvote upvote = db.Upvote.Find(id);
            Upvote upvote = m_DataBase.GetSingle(id);
            if (upvote == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Project, "Id", "Name", upvote.ProjectId);
            return View(upvote);
        }

        // POST: /Upvote/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,ProjectId,User,Date")] Upvote upvote)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(upvote).State = EntityState.Modified;
                //db.SaveChanges();
                m_DataBase.Update(upvote);
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Project, "Id", "Name", upvote.ProjectId);
            return View(upvote);
        }

        // GET: /Upvote/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Upvote upvote = db.Upvote.Find(id);
            Upvote upvote = m_DataBase.GetSingle(id);
            if (upvote == null)
            {
                return HttpNotFound();
            }
            return View(upvote);
        }

        // POST: /Upvote/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Upvote upvote = db.Upvote.Find(id);
            //db.Upvote.Remove(upvote);
            //db.SaveChanges();
            Upvote upvote = m_DataBase.GetSingle(id);
            if(upvote != null)
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