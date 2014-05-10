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
    public class CategoryController : Controller
    {
        ICategoryRepository m_CategoryRepository;
        //IProjectRepository m_ProjectRepository;
        //private TextisModelContainer db = new TextisModelContainer();

        public CategoryController()
        {
            m_CategoryRepository = new CategoryRepository();
            //m_ProjectRepository = new ProjectRepository();
            //db = new TextisModelContainer();
        }

        // GET: /Category/
        public ActionResult Index()
        {
            return View(m_CategoryRepository.GetAll().ToList());
        }

        // GET: /Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = m_CategoryRepository.GetSingle(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: /Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name")] Category category)
        {

            if (ModelState.IsValid)
            {
                m_CategoryRepository.Create(category);
                //db.SaveChanges();
                m_CategoryRepository.Save();

                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: /Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = m_CategoryRepository.GetSingle(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: /Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(category).State = EntityState.Modified;
                m_CategoryRepository.Update(category);
                //db.SaveChanges();
                m_CategoryRepository.Save();

                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: /Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = m_CategoryRepository.GetSingle(id);
            //Category category = db.Category.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: /Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = m_CategoryRepository.GetSingle(id);
            //db.Category.Remove(category);
            //db.SaveChanges();
            if (category != null)
            {
                m_CategoryRepository.Delete(id);
                m_CategoryRepository.Save();
            }
            else
            {
                return HttpNotFound();
            }
            
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            m_CategoryRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}
