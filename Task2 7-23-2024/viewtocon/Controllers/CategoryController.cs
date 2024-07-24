using System.Collections.Generic;
using System.Web.Mvc;

public class CategoryController : Controller
{
    private static List<Dictionary<string, object>> Contacts = new List<Dictionary<string, object>>();

    [HttpGet]
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(string Name, string Email, string PhoneNumber, string Subject, string Message)
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
            return RedirectToAction("Display", new { name = Name, email = Email, phoneNumber = PhoneNumber, subject = Subject, message = Message });
        }
        else
        {
            ViewBag.Message = "Invalid input. Please fill out all fields.";
            ViewBag.status = "fail";
        }

        return View();
    }

    [HttpGet]
    public ActionResult Display(string name, string email, string phoneNumber, string subject, string message)
    {
        ViewBag.Name = name;
        ViewBag.Email = email;
        ViewBag.PhoneNumber = phoneNumber;
        ViewBag.Subject = subject;
        ViewBag.Message = message;
        return View();
    }
}