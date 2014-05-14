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
    [Authorize]
    public class CommentController : Controller
    {
        ICommentRepository m_CommentRepository;
        IProjectRepository m_ProjectRepository;
        private List<CommentViewModel> m_CommentViewModelList;

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
        }

        // GET: /Comment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Comment comment = m_CommentRepository.GetSingle(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(new CommentViewModel(comment));
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
        public ActionResult Create([Bind(Include="Id,ProjectId,Text,User,Date")] CommentViewModel commentViewModel)
        {
            Comment comment = new Comment();
            comment = commentViewModel.CastViewModelToModel();
            comment.User = GetUsername();
            comment.Date = DateTime.Now;

            if (ModelState.IsValid)
            {                
                m_CommentRepository.Create(comment);
                m_CommentRepository.Save();

                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(m_ProjectRepository.GetAll(), "Id", "Name", commentViewModel.ProjectId);
            return View(commentViewModel);
        }

        // GET: /Comment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = m_CommentRepository.GetSingle(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(m_ProjectRepository.GetAll(), "Id", "Name", comment.ProjectId);
            return View(new CommentViewModel(comment));
        }

        // POST: /Comment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,ProjectId,Text,User,Date,ProjectName")] CommentViewModel commentViewModel)
        {
            if (ModelState.IsValid)
            {
                Comment comment = new Comment();
                comment = commentViewModel.CastViewModelToModel();
                comment.User = GetUsername();
                comment.Date = DateTime.Now;
                m_CommentRepository.Update(comment);
                m_CommentRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(m_ProjectRepository.GetAll(), "Id", "Name", commentViewModel.ProjectId);
            return View(commentViewModel);
        }

        // GET: /Comment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = m_CommentRepository.GetSingle(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(new CommentViewModel(comment));
        }

        // POST: /Comment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
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

        [HttpPost]
        public ActionResult AddComment(FormCollection formData)
        {
            Comment comment = new Comment();
            var tempid = Int32.Parse(formData["ProjectId"]);

            comment.Text = formData["Text"];
            comment.User = GetUsername();
            comment.Date = DateTime.Now;
            comment.ProjectId = tempid;

            m_CommentRepository.Create(comment);
            m_CommentRepository.Save();

            return Json(m_CommentRepository.GetByProjectId(tempid), JsonRequestBehavior.AllowGet);
        }
    }
}
