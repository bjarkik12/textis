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

        // GET: /Upvote/
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
            return View(new UpvoteViewModel(upvote));
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
            //return View(upvoteViewModel);
            return View("Index");
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
            return View(new UpvoteViewModel(upvote));
        }

        // POST: /Upvote/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

            return View(new UpvoteViewModel(upvote));
        }

        // POST: /Upvote/Delete/5
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

        [HttpPost]
        public ActionResult PostUpvote(Upvote upvote)
        {
            Upvote tempUpvote = new Upvote();
            tempUpvote.ProjectId = upvote.Id;
            tempUpvote.User = GetUsername();
            tempUpvote.Date = DateTime.Now;
            m_UpvoteRepository.Create(tempUpvote);
            m_UpvoteRepository.Save();
            return Json(JsonRequestBehavior.AllowGet);
        }

        //public PartialViewResult GetList()
        //{
        //    //var result = from c in m_CommentRepository.GetByProjectId(id)
        //    //             orderby c.Date ascending
        //    //             select c;
        //    List<ProjectViewModel> tempList = new List<ProjectViewModel>();
        //    //foreach (Comment tempComment in result)
        //    foreach (Project tempProject in m_ProjectRepository.GetAll())
        //    {
        //        tempList.Add(new ProjectViewModel(tempProject));
        //    }
        //    return PartialView("index", tempList);
        //}

        protected override void Dispose(bool disposing)
        {
            m_UpvoteRepository.Dispose();
            m_ProjectRepository.Dispose();
            base.Dispose(disposing);

        }
    }
}
