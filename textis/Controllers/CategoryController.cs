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
    public class CategoryController : Controller
    {
        private ICategoryRepository m_CategoryRepository;
        private CategoryViewModel m_CategoryViewModel;
        private List<CategoryViewModel> m_CategoryViewModelList;
        //IProjectRepository m_ProjectRepository;
        //private TextisModelContainer db = new TextisModelContainer();

        public CategoryController()
        {
            m_CategoryRepository = new CategoryRepository();
            m_CategoryViewModel = new CategoryViewModel();
            m_CategoryViewModelList = new List<CategoryViewModel>();
            //m_ProjectRepository = new ProjectRepository();
            //db = new TextisModelContainer();
        }

        // GET: /Category/
        public ActionResult Index()
        {
            foreach (Category x in m_CategoryRepository.GetAll().ToList())
            {
                CategoryViewModel categoryViewModel = new CategoryViewModel(x);
                m_CategoryViewModelList.Add(categoryViewModel);
            }
            return View(m_CategoryViewModelList);            
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
            return View(new CategoryViewModel(category));
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
        public ActionResult Create([Bind(Include="Id,Name")] CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                Category category = new Category();
                category = categoryViewModel.CastViewModelToModel();
                m_CategoryRepository.Create(category);
                //db.SaveChanges();
                m_CategoryRepository.Save();

                return RedirectToAction("Index");
            }

            return View(categoryViewModel);
        }

        // GET: /Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = m_CategoryRepository.GetSingle(id);
            CategoryViewModel categoryViewModel = new CategoryViewModel(category);
            if (categoryViewModel == null)
            {
                return HttpNotFound();
            }
            return View(categoryViewModel);
        }

        // POST: /Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name")] CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                Category category = new Category();
                category = categoryViewModel.CastViewModelToModel();
                m_CategoryRepository.Update(category);
                m_CategoryRepository.Save();

                return RedirectToAction("Index");
            }
            return View(categoryViewModel);
        }

        // GET: /Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = m_CategoryRepository.GetSingle(id);
            CategoryViewModel categoryViewModel = new CategoryViewModel(category);
            if (categoryViewModel == null)
            {
                return HttpNotFound();
            }
            return View(categoryViewModel);
        }

        // POST: /Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = m_CategoryRepository.GetSingle(id);
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
