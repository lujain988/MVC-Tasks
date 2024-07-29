using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Task_5_7_28_2024.Models;

namespace Task_5_7_28_2024.Controllers
{
    public class datastudentsController : Controller
    {
        private StudentsEntities db = new StudentsEntities();

        // GET: datastudents
        public ActionResult Index()
        {
            return View(db.datastudents.ToList());
        }

        // GET: datastudents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            datastudent datastudent = db.datastudents.Find(id);
            if (datastudent == null)
            {
                return HttpNotFound();
            }
            return View(datastudent);
        }

        // GET: datastudents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: datastudents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Email,Image,PhoneNumber,State,City")] datastudent datastudent, HttpPostedFileBase upload){
            if (upload != null && upload.ContentLength > 0)
            {
                var fileName = Path.GetFileName(upload.FileName);
                var path = Path.Combine(Server.MapPath("~/image"), fileName);


                if (!Directory.Exists(Server.MapPath("~/image")))
                {
                    Directory.CreateDirectory(Server.MapPath("~/image"));
                }
            }
            if (ModelState.IsValid)
            {
                db.datastudents.Add(datastudent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(datastudent);
        }

        // GET: datastudents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            datastudent datastudent = db.datastudents.Find(id);
            if (datastudent == null)
            {
                return HttpNotFound(":))))))");
            }
            return View(datastudent);
        }

        // POST: datastudents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Email,Image,PhoneNumber,State,City")] datastudent datastudent, HttpPostedFileBase upload)
        {
            if (upload != null && upload.ContentLength > 0)
            {
                var fileName = Path.GetFileName(upload.FileName);
                var path = Path.Combine(Server.MapPath("~/image"), fileName);


                if (!Directory.Exists(Server.MapPath("~/image")))
                {
                    Directory.CreateDirectory(Server.MapPath("~/image"));
                }
                upload.SaveAs(path);
                datastudent.Image = fileName;
            }
         
            if (ModelState.IsValid)
            {
                db.Entry(datastudent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(datastudent);
        }

        public ActionResult Delete(int? id)
        {
            datastudent datastudent = db.datastudents.Find(id);
            db.datastudents.Remove(datastudent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: datastudents/Delete/5
        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
