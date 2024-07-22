using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Task1_7_22_2024.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Services()
        {
            return View();
        }

        public ActionResult Portfolio()
        {
            return View();
        }

        public ActionResult Blog()
        {
            return View();
        }

        public ActionResult Venues()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        // Action for VenueDetails
        public ActionResult VenueDetails(int id)
        {
            // Logic to get venue details by id
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

      

      



    }
}