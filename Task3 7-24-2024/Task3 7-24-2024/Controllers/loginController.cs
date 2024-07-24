using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Task3_7_24_2024.Controllers
{
    public class LoginController : Controller
    {
        // GET: login
        public ActionResult Index()
        {
            if (Session["Username"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (Session["Username"] != null)
            {
                ViewBag.Message = "Hi";
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            // Check the specified credentials
            if (username == "Lujain@gmail.com" && password == "1234")
            {
                Session["Username"] = username;
                TempData["Message"] = "Login successful.";
                TempData["status"] = "success";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Invalid username or password.";
                ViewBag.status = "fail";
                return View();
            }
        }

     

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}