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
    public class TeachersController : Controller
    {
        private ApplacationDbContext db = new ApplacationDbContext();

        // GET: Teachers
        public ActionResult Index()
        {
            return View(db.Teachers.Include(a=>a.Courses).ToList());
        }

        // GET: Teachers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // GET: Teachers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TeacherName,age")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Teachers.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Include(t => t.Courses).SingleOrDefault(t => t.ID == id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Teacher teacher, int[] selectedCourses)
        {
            if (ModelState.IsValid)
            {
                var existingTeacher = db.Teachers.Include(t => t.Courses).SingleOrDefault(t => t.ID == teacher.ID);

                if (existingTeacher == null)
                {
                    return HttpNotFound();
                }

                // Update Teacher properties
                existingTeacher.TeacherName = teacher.TeacherName;
                existingTeacher.age = teacher.age;

                // Track courses that should be removed
                var coursesToRemove = existingTeacher.Courses
                    .Where(c => !selectedCourses.Contains(c.ID))
                    .ToList();

                // Remove courses
                foreach (var course in coursesToRemove)
                {
                    existingTeacher.Courses.Remove(course);
                }

                // Add new selected courses
                foreach (var courseId in selectedCourses)
                {
                    if (!existingTeacher.Courses.Any(c => c.ID == courseId))
                    {
                        var course = db.Courses.Find(courseId);
                        if (course != null)
                        {
                            existingTeacher.Courses.Add(course);
                        }
                    }
                }

                // Update the state and save changes
                db.Entry(existingTeacher).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("", "Unable to save changes. The record may have been modified or deleted.");
                    return View(teacher);
                }

                return RedirectToAction("Index");
            }

            return View(teacher);
        }



        // GET: Teachers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            db.Teachers.Remove(teacher);
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
