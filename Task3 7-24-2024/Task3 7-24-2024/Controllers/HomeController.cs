using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Task3_7_24_2024.Controllers
{
    public class HomeController : Controller
    {
        // Simulating a database with a static list
        private static List<Dictionary<string, object>> Contacts = new List<Dictionary<string, object>>();

        public ActionResult Index()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
                ViewBag.status = TempData["status"];
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(string Name, string Email, string PhoneNumber, string Subject, string Message)
        {
            if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(PhoneNumber) && !string.IsNullOrEmpty(Subject) && !string.IsNullOrEmpty(Message))
            {
                var contact = new Dictionary<string, object>
                {
                    { "Name", Name },
                    { "Email", Email },
                    { "PhoneNumber", PhoneNumber },
                    { "Subject", Subject },
                    { "Message", Message }
                };

                Contacts.Add(contact);
                ViewBag.Message = "Your message has been sent successfully.";
                ViewBag.status = "success";
                ViewBag.ContactDetails = contact;

            }
            else
            {
                ViewBag.Message = "Invalid input. Please fill out all fields.";
                ViewBag.status = "fail";
            }

            return View();
        }

        public ActionResult ContactDetails(string name, string email, string phoneNumber, string subject, string message)
        {
            ViewBag.Name = name;
            ViewBag.Email = email;
            ViewBag.PhoneNumber = phoneNumber;
            ViewBag.Subject = subject;
            ViewBag.Message = message;
            return View();
        }

        public ActionResult OurStory()
        {
            ViewBag.Message = "Your OurStory page.";
            return View();
        }
    }
}
