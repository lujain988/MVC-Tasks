using System.Collections.Generic;
using System.Web.Mvc;

public class CategoryController : Controller
{
    // Simulating a database with a static list
    private static List<Dictionary<string, object>> Items = new List<Dictionary<string, object>>();

    [HttpGet]
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(int? ID, string Name, string Gender, string Category, string[] Preferences)
    {
        if (ID.HasValue && !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Gender) && !string.IsNullOrEmpty(Category) && Preferences != null)
        {
            var item = new Dictionary<string, object>
            {
                { "ID", ID },
                { "Name", Name },
                { "Gender", Gender },
                { "Category", Category },
                { "Preferences", Preferences }
            };

            Items.Add(item);
            ViewBag.Message = "Answer has been added";
            ViewBag.status = "success";
            return RedirectToAction("Display", new { id = ID, name = Name, gender = Gender, category = Category, preferences = string.Join(",", Preferences) });

        }
        else
        {
            ViewBag.Message = "Invalid input. Please fill out all fields.";
            ViewBag.status = "fail";
        }

        return View();
    }

    [HttpGet]
    public ActionResult Display(int? id, string name, string gender, string category, string preferences)
    {
        ViewBag.ID = id;
        ViewBag.Name = name;
        ViewBag.Gender = gender;
        ViewBag.Category = category;
        ViewBag.Preferences = preferences?.Split(',') ?? new string[0];
        return View();
    }
}