using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication11.Models;

namespace WebApplication11.Controllers
{
    public class ProdController : Controller
    {
        [HttpGet]
        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(string name, long price, string imageUrl)
        {
            try
            {
                var products = Session["Products"] as List<Add.AddProduct>;
                if (products == null)
                {
                    products = new List<Add.AddProduct>();
                }
                var product = new Add.AddProduct
                {
                    Id = products.Count + 1,  
                    Name = name,
                    Price = price,
                    ImageUrl = imageUrl
                };

                products.Add(product);

                Session["Products"] = products;
                return RedirectToAction("Shop");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Shop()
        {
            var products = Session["Products"] as List<Add.AddProduct>;

            ViewBag.Products = products;

            return View(products);
        }
    }
}
