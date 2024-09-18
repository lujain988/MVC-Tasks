using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Code.Models;

namespace Code.Controllers
{
    public class studentDetailsController : Controller
    {
        private ApplacationDbContext db = new ApplacationDbContext();

        // GET: studentDetails
        public ActionResult Index()
        {
            var studentDetails = db.StudentDetails.Include(s => s.Students);
            return View(studentDetails.ToList());
        }

        // GET: studentDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            studentDetails studentDetails = db.StudentDetails.Find(id);
            if (studentDetails == null)
            {
                return HttpNotFound();
            }
            return View(studentDetails);
        }

        // GET: studentDetails/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.Students, "ID", "StudentName");
            return View();
        }

        // POST: studentDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FatherName,MotherName,NumOfSiblings")] studentDetails studentDetails)
        {
            if (ModelState.IsValid)
            {
                db.StudentDetails.Add(studentDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.Students, "ID", "StudentName", studentDetails.ID);
            return View(studentDetails);
        }

        // GET: studentDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            studentDetails studentDetails = db.StudentDetails.Find(id);
            if (studentDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.Students, "ID", "StudentName", studentDetails.ID);
            return View(studentDetails);
        }

        // POST: studentDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FatherName,MotherName,NumOfSiblings")] studentDetails studentDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.Students, "ID", "StudentName", studentDetails.ID);
            return View(studentDetails);
        }

        // GET: studentDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            studentDetails studentDetails = db.StudentDetails.Find(id);
            if (studentDetails == null)
            {
                return HttpNotFound();
            }
            return View(studentDetails);
        }

        // POST: studentDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            studentDetails studentDetails = db.StudentDetails.Find(id);
            db.StudentDetails.Remove(studentDetails);
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
