﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using textis.Repository;
using textis.ViewModel;
namespace textis.Controllers
{
    public class HomeController : Controller
    {         
        public ActionResult Index(string category, string searchString)
        {
            IProjectRepository m_ProjectRepository = new ProjectRepository();
            List<ProjectViewModel> m_ProjectViewModelList = new List<ProjectViewModel>();
            var categoryList = new List<string>();

            var categoryQuery = from n in m_ProjectRepository.GetAll()
                                orderby n.Category.Name
                                select n.Category.Name;

            categoryList.AddRange(categoryQuery.Distinct());
            ViewBag.category = new SelectList(categoryList);

            var project = from m in m_ProjectRepository.GetAll()
                          select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                project = project.Where(s => s.Name.ToLower().Contains(searchString.ToLower()));
            }

            if (!string.IsNullOrEmpty(category))
            {
                project = project.Where(x => x.Category.Name == category);
            }

            foreach (Project x in project.ToList())
            {
                ProjectViewModel projectViewModel = new ProjectViewModel(x);
                m_ProjectViewModelList.Add(projectViewModel);
            }

            return View(m_ProjectViewModelList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult faq()
        {
            ViewBag.Message = "Leiðbeiningar.";

            return View();
        }
    }
}