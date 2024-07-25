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
    public ActionResult Create(int Number, string Name, string PhoneNumber, string Subject, string Message, string Gender, string Category, List<string> Preferences)
    {
        if (Number > 0 && !string.IsNullOrEmpty(Name)  && !string.IsNullOrEmpty(Subject)  && !string.IsNullOrEmpty(Gender) && !string.IsNullOrEmpty(Category))
        {
            var contact = new Dictionary<string, object>
            {
                { "Number", Number },
                { "Name", Name },
                { "Subject", Subject },
                { "Gender", Gender },
                { "Category", Category },
                { "Preferences", Preferences }
            };

            Contacts.Add(contact);
            ViewBag.Message = "Your message has been sent successfully.";
            ViewBag.status = "success";
            return RedirectToAction("Display", new { number = Number, name = Name,  subject = Subject, gender = Gender, category = Category, preferences = string.Join(", ", Preferences) });
        }
        else
        {
            ViewBag.Message = "Invalid input. Please fill out all fields.";
            ViewBag.status = "fail";
        }

        return View();
    }

    [HttpGet]
    public ActionResult Display(int number, string name,  string subject,  string gender, string category, string preferences)
    {
        ViewBag.Number = number;
        ViewBag.Name = name;
        ViewBag.Subject = subject;
        ViewBag.Gender = gender;
        ViewBag.Category = category;
        ViewBag.Preferences = preferences;
        return View();
    }
}
