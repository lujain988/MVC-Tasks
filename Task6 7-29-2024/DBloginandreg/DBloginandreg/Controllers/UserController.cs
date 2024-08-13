using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DBloginandreg.Models;

namespace DBloginandreg.Controllers
{
    public class UserController : Controller
    {
        private RegistrationDBEntities dbmodel = new RegistrationDBEntities();

        // GET: User/AddOrEdit
        public ActionResult AddOrEdit(int id = 0)
        {
            User userModel = new User();
            if (id != 0)
            {

                userModel = dbmodel.Users.Where(x => x.UserID == id).FirstOrDefault();
            }
            return View(userModel);
        }

        // POST: User/AddOrEdit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEdit(User userModel , string confirmPassword)
        {
            if (ModelState.IsValid)
            {
                var existingUser = dbmodel.Users.FirstOrDefault(u => u.Email == userModel.Email && u.UserID != userModel.UserID);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "Email already exists");
                    return View(userModel);
                }
                if (userModel.Password != confirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "The password and confirmation password do not match.");
                    return View(userModel);
                }
                dbmodel.Users.AddOrUpdate(userModel);
                dbmodel.SaveChanges();

                ModelState.Clear();
                ViewBag.SuccessMessage = "User information saved successfully.";
                return RedirectToAction("Login");
                // Clear the form by returning a new User model
            }

            return View(userModel); // Return the same view with validation messages if the model state is not valid
        }


        // GET: User/Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }



        public ActionResult Login(User userModel)
        {
            var checkLogin = dbmodel.Users
                .FirstOrDefault(x => x.Email == userModel.Email && x.Password == userModel.Password);
            if (checkLogin != null)
            {
                Session["UserID"] = checkLogin.UserID.ToString();
                return RedirectToAction("Profile", "User");

            }
            else
            {
                ViewBag.Notification = "Wrong Email or password";
            }

            return View();
        }

        // GET: User/Logout
        public ActionResult Logout()
        {
            Session.Clear(); // Clear the session
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Profile(HttpPostedFileBase imageFile)
        {
            if (Session["UserID"] != null)
            {
                int userID = int.Parse(Session["UserID"].ToString());
                User userModel = dbmodel.Users.FirstOrDefault(u => u.UserID == userID);
                return View(userModel);
            }
            return RedirectToAction("Login");


        }
        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = dbmodel.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                //if (!string.IsNullOrEmpty(user.NewPassword) && !string.IsNullOrEmpty(user.ConfirmPassword))
                //{
                //    if (user.NewPassword == user.ConfirmPassword)
                //    {
                //        user.Password = user.NewPassword;
                //    }
                //    else
                //    {
                //        ModelState.AddModelError("ConfirmPassword", "The new password and confirmation password do not match.");
                //        return View(user);
                //    }
                //}

                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    // Generate unique file name
                    string fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(imageFile.FileName);
                    string filePath = Server.MapPath("~/Content/Images/") + fileName;

                    // Save file to the server
                    imageFile.SaveAs(filePath);

                    // Update user's image path
                    user.Image = fileName;
                }

                dbmodel.Entry(user).State = EntityState.Modified;
                dbmodel.SaveChanges();
                TempData["SuccessMessage"] = "Profile updated successfully.";
                return RedirectToAction("Profile");
            }




            return View(user);
        }


 

            // GET: User/ResetPassword
            public ActionResult ResetPassword()
            {
                var userId = Session["UserID"];
                if (userId == null)
                {
                    return RedirectToAction("Login");
                }
                return View();
            }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(string oldPassword, string newPassword, string confirmPassword)
        {
            var userIdString = Session["UserID"];
            if (userIdString == null)
            {
                return RedirectToAction("Login");
            }

            int userId;
            if (!int.TryParse(userIdString.ToString(), out userId))
            {
                // Handle the case where the session value is not a valid integer
                return RedirectToAction("Login");
            }

            var user = dbmodel.Users.Find(userId);
            if (user == null)
            {
                return HttpNotFound();
            }

            if (user.Password != oldPassword)
            {
                ModelState.AddModelError("", "Old password is incorrect.");
                return View();
            }

            if (newPassword != confirmPassword)
            {
                ModelState.AddModelError("", "New password and confirmation password do not match.");
                return View();
            }

            user.Password = newPassword;
            dbmodel.Entry(user).State = EntityState.Modified;
            dbmodel.SaveChanges();

            ViewBag.SuccessMessage = "Password has been reset successfully.";
            return RedirectToAction("Profile");
        }

    }


}
