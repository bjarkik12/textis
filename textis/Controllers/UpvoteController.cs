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
    public class UpvoteController : Controller
    {
        IUpvoteRepository m_UpvoteRepository;
        IProjectRepository m_ProjectRepository;
        

        public UpvoteController()
        {
            m_UpvoteRepository = new UpvoteRepository();
            m_ProjectRepository = new ProjectRepository();
        }

        public UpvoteController(IUpvoteRepository repository)
        {
            // only used in UnitTesting
            m_UpvoteRepository = repository;
            m_ProjectRepository = new ProjectRepository();
        }

        public UpvoteController(IUpvoteRepository upvoteRepository, IProjectRepository projectRepository)
        {
            // only used in UnitTesting
            m_UpvoteRepository = upvoteRepository;
            m_ProjectRepository = projectRepository;
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
            List<UpvoteViewModel> m_UpvoteViewModelList = new List<UpvoteViewModel>();

            foreach (Upvote x in m_UpvoteRepository.GetAll().ToList())
            {
                UpvoteViewModel upvoteViewModel = new UpvoteViewModel(x);
                m_UpvoteViewModelList.Add(upvoteViewModel);
            }

            return View(m_UpvoteViewModelList);
        }

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
            return View(new UpvoteViewModel(upvote));
        }

        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(m_ProjectRepository.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProjectId,User,Date")] UpvoteViewModel upvoteViewModel)
        {
            if (ModelState.IsValid)
            {
                Upvote upvote = new Upvote();
                upvote = upvoteViewModel.CastViewModelToModel();
                upvote.User = GetUsername();
                upvote.Date = DateTime.Now;
                m_UpvoteRepository.Create(upvote);
                m_UpvoteRepository.Save();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(m_ProjectRepository.GetAll(), "Id", "Name", upvoteViewModel.ProjectId);
            return View("Index");
        }

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
            return View(new UpvoteViewModel(upvote));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProjectId,User,Date")] UpvoteViewModel upvoteViewModel)
        {
            if (ModelState.IsValid)
            {
                Upvote upvote = new Upvote();
                upvote = upvoteViewModel.CastViewModelToModel();
                upvote.User = GetUsername();
                upvote.Date = DateTime.Now;
                m_UpvoteRepository.Update(upvote);
                m_UpvoteRepository.Save();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(m_ProjectRepository.GetAll(), "Id", "Name", upvoteViewModel.ProjectId);
            return View(upvoteViewModel);

        }

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

            return View(new UpvoteViewModel(upvote));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Upvote upvote = m_UpvoteRepository.GetSingle(id);
            if (upvote != null)
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
        /// <summary>
        /// Accepts parameter from upvote function in scrip_textis.js
        /// </summary>
        /// <param name="upvote"></param>
        /// <returns>Json</returns>
        [HttpPost]
        public ActionResult PostUpvote(Upvote upvote)
        {
            Upvote newUpvote = new Upvote();
            newUpvote.ProjectId = upvote.Id;
            newUpvote.User = GetUsername();
            newUpvote.Date = DateTime.Now;
            m_UpvoteRepository.Create(newUpvote);
            m_UpvoteRepository.Save();
            return Json(JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            m_UpvoteRepository.Dispose();
            m_ProjectRepository.Dispose();
            base.Dispose(disposing);

        }
    }
}
