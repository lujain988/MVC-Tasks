using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Code.Models;

namespace Code.Controllers
{
    public class StudentsController : Controller
    {
        private ApplacationDbContext db = new ApplacationDbContext();

        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.StudentDetails);
            return View(students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students students = db.Students.Include(s => s.StudentDetails).SingleOrDefault(s => s.ID == id);
            if (students == null)
            {
                return HttpNotFound();
            }
            return View(students);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.StudentDetailsID = new SelectList(db.StudentDetails, "ID", "FatherName");
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,StudentName,Age,StudentDetailsID")] Students students)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(students);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentDetailsID = new SelectList(db.StudentDetails, "ID", "FatherName", students.ID);
            return View(students);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Retrieve the student along with related studentDetails
            var student = db.Students.Include(s => s.StudentDetails).SingleOrDefault(s => s.ID == id);

            if (student == null)
            {
                return HttpNotFound();
            }

            // Pass the student model to the view
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Students student)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the existing student including its details
                var existingStudent = db.Students.Include(s => s.StudentDetails).SingleOrDefault(s => s.ID == student.ID);

                if (existingStudent == null)
                {
                    return HttpNotFound();
                }

                // Update Student properties
                existingStudent.StudentName = student.StudentName;
                existingStudent.Age = student.Age;

                // Update or add StudentDetails
                if (existingStudent.StudentDetails == null)
                {
                    existingStudent.StudentDetails = new studentDetails
                    {
                        ID = existingStudent.ID
                    };
                }

                existingStudent.StudentDetails.FatherName = student.StudentDetails.FatherName;
                existingStudent.StudentDetails.MotherName = student.StudentDetails.MotherName;
                existingStudent.StudentDetails.NumOfSiblings = student.StudentDetails.NumOfSiblings;

                // Attach the entities and set their state to modified
                db.Entry(existingStudent).State = EntityState.Modified;
                db.Entry(existingStudent.StudentDetails).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Handle concurrency issue
                    ModelState.AddModelError("", "Unable to save changes. The record may have been modified or deleted.");
                    return View(student);
                }

                return RedirectToAction("Index");
            }

            return View(student);
        }








        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students students = db.Students.Find(id);
            if (students == null)
            {
                return HttpNotFound();
            }
            return View(students);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Students students = db.Students.Find(id);
            db.Students.Remove(students);
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
