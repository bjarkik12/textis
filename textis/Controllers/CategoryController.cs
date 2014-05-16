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
    /// <summary>
    /// Only Administrators are allowed to Edit, Add or Remove Categories
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private ICategoryRepository m_CategoryRepository;

        public CategoryController()
        {
            m_CategoryRepository = new CategoryRepository();
        }

        public CategoryController(ICategoryRepository repository)
        {
            m_CategoryRepository = repository;
        }

        public ActionResult Index()
        {
            List<CategoryViewModel> m_CategoryViewModelList = new List<CategoryViewModel>();

            foreach (Category x in m_CategoryRepository.GetAll().ToList())
            {
                CategoryViewModel categoryViewModel = new CategoryViewModel(x);
                m_CategoryViewModelList.Add(categoryViewModel);
            }

            return View(m_CategoryViewModelList);            
        }

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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name")] CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                Category category = new Category();
                category = categoryViewModel.CastViewModelToModel();
                m_CategoryRepository.Create(category);
                m_CategoryRepository.Save();
                return RedirectToAction("Index");
            }

            return View(categoryViewModel);
        }

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
