using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace textis.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Um Síðuna";

            return View();
        }

        public ActionResult Faq()
        {
            ViewBag.Message = "Algengar Spurningar";

            return View();
        }

        public ActionResult RequestSubtitle()
        {
            ViewBag.Message = "Textabeiðni";

            return View();
        }
    }
}