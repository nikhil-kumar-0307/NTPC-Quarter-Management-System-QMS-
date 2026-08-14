using QMS.Data;
using QMS.Models.DTOs;
using System;
using System.Linq;
using System.Web.Mvc;

namespace QMS.Controllers
{
    public class AccountController : Controller
    {
        private readonly QMSDbContext _db = new QMSDbContext();

        // GET: Account/Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginDto model)
        {
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }

           
            var user = _db.Users.FirstOrDefault(
                u => u.EmployeeNumber == model.EmployeeNumber
            );

            
            if (user == null)
            {
                ModelState.AddModelError(
                    "",
                    "Invalid Employee Number or Password."
                );

                return View(model);
            }
           
            bool passwordValid = model.Password == user.PasswordHash;

            if (!passwordValid)
            {
                ModelState.AddModelError(
                    "",
                    "Invalid Employee Number or Password."
                );

                return View(model);
            }

            
            user.LastLoginAt = DateTime.Now;

            _db.SaveChanges();

            // 6. Store user information in Session
            Session["UserId"] = user.Id;
            Session["EmployeeNumber"] = user.EmployeeNumber;
            Session["Role"] = user.Role;

            // 7. Redirect according to role
            if (user.Role == "Admin")
            {
                return RedirectToAction(
                    "AdminDashboard",
                    "Dashboard"
                );
            }

            // If another role logs in
            return RedirectToAction(
                "AdminDashboard",
                "Dashboard"
            );
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}