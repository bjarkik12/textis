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
using textis.HelpFunction;
using textis.ViewModel;

namespace textis.Controllers
{
    public class CommentController : Controller
    {
        //private TextisModelContainer db = new TextisModelContainer();

        ICommentRepository m_CommentRepository;
        IProjectRepository m_ProjectRepository;
        private List<CommentViewModel> m_CommentViewModelList;

        //ICommentRepository m_DataBase;
        //TextisModelContainer db;
        //private TextisModelContainer db;


        public string GetUsername(){
            if (Request.IsAuthenticated)
            {
                return User.Identity.Name;
            }
            else
            {
                return "Nafnlaus";
            }
        }
        
        public CommentController()
        {
            m_CommentRepository = new CommentRepository();
            m_ProjectRepository = new ProjectRepository();
            m_CommentViewModelList = new List<CommentViewModel>();
            //db = new TextisModelContainer();
        }


        // GET: /Comment/
        public ActionResult Index()
        {
            foreach (Comment x in m_CommentRepository.GetAll().ToList())
            {
                CommentViewModel commentViewModel = new CommentViewModel(x);
                m_CommentViewModelList.Add(commentViewModel);
            }
            return View(m_CommentViewModelList);            

            
            
            //var comment = db.Comment.Include(c => c.Project);
            //return View(comment.ToList());
            //var comment = m_CommentRepository.GetAll();
            //return View(comment.ToList());
        }

        // GET: /Comment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Comment comment = db.Comment.Find(id);
            Comment comment = m_CommentRepository.GetSingle(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: /Comment/Create
        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(m_ProjectRepository.GetAll(), "Id", "Name");
            return View();
        }

        // POST: /Comment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,ProjectId,Text,User,Date")] Comment comment)
        {
            comment.User = GetUsername();
            comment.Date = DateTime.Now;

            if (ModelState.IsValid)
            {
                //db.Comment.Add(comment);
                //db.SaveChanges();
                
                m_CommentRepository.Create(comment);
                m_CommentRepository.Save();

                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(m_ProjectRepository.GetAll(), "Id", "Name", comment.ProjectId);
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
            Comment comment = m_CommentRepository.GetSingle(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(m_ProjectRepository.GetAll(), "Id", "Name", comment.ProjectId);
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
                comment.User = GetUsername();
                m_CommentRepository.Update(comment);
                m_CommentRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(m_ProjectRepository.GetAll(), "Id", "Name", comment.ProjectId);
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
            Comment comment = m_CommentRepository.GetSingle(id);
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
            Comment comment = m_CommentRepository.GetSingle(id);
            if (comment != null)
            {
                m_CommentRepository.Delete(id);
                m_CommentRepository.Save();
            }
            else
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            m_CommentRepository.Dispose();
            m_ProjectRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}
