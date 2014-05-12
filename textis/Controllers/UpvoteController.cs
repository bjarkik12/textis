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
    public class UpvoteController : Controller
    {
        //private TextisModelContainer db = new TextisModelContainer();
        IUpvoteRepository m_UpvoteRepository;
        IProjectRepository m_ProjectRepository;

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

        public UpvoteController()
        {
            m_UpvoteRepository = new UpvoteRepository();
            m_ProjectRepository = new ProjectRepository();
        }

        // GET: /Upvote/
        public ActionResult Index()
        {
            var comment = m_UpvoteRepository.GetAll();
            return View(comment.ToList());
        }

        // GET: /Upvote/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Upvote upvote = m_UpvoteRepository.GetSingle(id);
            if (upvote == null)
            {
                return HttpNotFound();
            }
            return View(upvote);
        }

        // GET: /Upvote/Create
        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(m_ProjectRepository.GetAll(), "Id", "Name");
            return View();
        }

        // POST: /Upvote/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,ProjectId,User,Date")] Upvote upvote)
        {
            upvote.User = GetUsername();
            upvote.Date = DateTime.Now;
            
            if (ModelState.IsValid)
            {               
                m_UpvoteRepository.Create(upvote);
                m_UpvoteRepository.Save();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(m_ProjectRepository.GetAll(), "Id", "Name", upvote.ProjectId);
            return View(upvote);
        }

        // GET: /Upvote/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Upvote upvote = m_UpvoteRepository.GetSingle(id);
            if (upvote == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(m_ProjectRepository.GetAll(), "Id", "Name", upvote.ProjectId);
            return View(upvote);
        }

        // POST: /Upvote/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,ProjectId,User,Date")] Upvote upvote)
        {
            upvote.User = GetUsername();
            upvote.Date = DateTime.Now;
            
            if (ModelState.IsValid)
            {
                m_UpvoteRepository.Update(upvote);
                m_UpvoteRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(m_ProjectRepository.GetAll(), "Id", "Name", upvote.ProjectId);
            return View(upvote);
        }

        // GET: /Upvote/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Upvote upvote = m_UpvoteRepository.GetSingle(id);
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
            Upvote upvote = m_UpvoteRepository.GetSingle(id);
            if(upvote != null)
            {
                m_UpvoteRepository.Delete(id);
                m_UpvoteRepository.Save();
            }
            else
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            m_UpvoteRepository.Dispose();
            m_ProjectRepository.Dispose();
            base.Dispose(disposing);
            
        }
    }
}
