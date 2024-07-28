using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
        public ActionResult Create([Bind(Include = "ID,Name,Email,PhoneNumber,Image,State,City")] datastudent datastudent)
        {
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
                return HttpNotFound();
            }
            return View(datastudent);
        }

        // POST: datastudents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Email,PhoneNumber,Image,State,City")] datastudent datastudent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(datastudent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(datastudent);
        }

        // GET: datastudents/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: datastudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            datastudent datastudent = db.datastudents.Find(id);
            db.datastudents.Remove(datastudent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
