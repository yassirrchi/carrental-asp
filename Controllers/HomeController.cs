using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarRentByYassirRchi.Models;

namespace CarRentByYassirRchi.Controllers
{
    public class HomeController : Controller
    {
        private CarRentEntities db = new CarRentEntities();
        public ActionResult Index()
        {
            return View(db.Voitures.ToList());
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
    }
}