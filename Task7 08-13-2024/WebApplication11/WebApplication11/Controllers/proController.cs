using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using WebApplication11.Models;

namespace WebApplication11.Controllers
{

    public class proController : Controller
    {
        private productsEntities db = new productsEntities();

        [HttpGet]
        public ActionResult AddProduct()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddProduct(AddProduct model,string name, long price, string imageUrl)
        {
            try
            {
               db.AddProducts.Add(model);
                db.SaveChanges();
                return RedirectToAction("shop");
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult Shop(int? id)
        {
            List<AddProduct> products;

            if (id == null)
            {
                products = db.AddProducts.ToList();
            }
            else
            {
                products = db.AddProducts.Where(p => p.ID == id).ToList();
            }
            return View(products);
        }



    }
}
