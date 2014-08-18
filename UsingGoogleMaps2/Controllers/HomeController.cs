using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UsingGoogleMaps2.Models;

namespace UsingGoogleMaps2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }
        [HttpPost]
        public ActionResult Index(AreaToSearsh arreaToSearch)
        {
             if (ModelState.IsValid)
            {


                return RedirectToAction("ShowPubsOnMap", "Maps", arreaToSearch);

            }

           
            return View();
        }

        

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
