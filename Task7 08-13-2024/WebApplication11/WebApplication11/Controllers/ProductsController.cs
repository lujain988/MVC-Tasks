using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WebApplication11.Controllers
{
    public class ProductsController : Controller
    {
    
        [HttpGet]
        public ActionResult AddProduct()
        {
            return View();
        }

     
        [HttpPost]
        public ActionResult AddProduct(string name, decimal price, string imageUrl)
        {
            try
            {
                var products = Session["Products"] as List<List<object>>;
                if (products == null)
                {
                    products = new List<List<object>>();
                }

                var product = new List<object> { name, imageUrl, price };
                products.Add(product);

                Session["Products"] = products;

                return RedirectToAction("shop");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Shop()
        {
            var products = Session["Products"] as List<List<object>>;
            ViewBag.Products = products;
            return View();
        }
    }
}
