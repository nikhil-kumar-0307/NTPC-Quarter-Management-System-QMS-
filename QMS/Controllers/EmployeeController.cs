using QMS.Data;
using QMS.Data.Models;
using QMS.Models.DTOs;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace QMS.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly QMSDbContext _db = new QMSDbContext();

        private static readonly string[] QuarterTypes = { "A", "B", "C" };
        private static readonly string[] StatusOptions = { "Active", "Empty", "Retained", "Agency" };

        // GET: Employee
        public ActionResult Index()
        {
            var employees = _db.Employees.OrderBy(e => e.Name).ToList();
            return View(employees);
        }

        // GET: Employee/Create
        [HttpGet]
        public ActionResult Create()
        {
            PopulateDropdowns();
            return View(new EmployeeDto());
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeDto model)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropdowns();
                return View(model);
            }

            var employee = new Employee
            {
                Name = model.Name,
                EmployeeNo = model.EmployeeNo,
                QuarterNo = model.QuarterNo,
                QuarterType = model.QuarterType,
                Status = model.Status,
                ResidenceTelNo = model.ResidenceTelNo,
                CreatedAt = DateTime.Now
            };

            if (model.ProfilePic != null && model.ProfilePic.ContentLength > 0)
            {
                var folder = Server.MapPath("~/Content/uploads/employees");
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                var fileName = Guid.NewGuid() + Path.GetExtension(model.ProfilePic.FileName);
                model.ProfilePic.SaveAs(Path.Combine(folder, fileName));
                employee.ProfilePicPath = "/Content/uploads/employees/" + fileName;
            }

            _db.Employees.Add(employee);
            _db.SaveChanges();

            TempData["Success"] = "Employee added successfully.";
            return RedirectToAction("Index");
        }

        private void PopulateDropdowns()
        {
            ViewBag.QuarterTypes = new SelectList(QuarterTypes);
            ViewBag.StatusOptions = new SelectList(StatusOptions);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _db.Dispose();
            base.Dispose(disposing);
        }
    }
}