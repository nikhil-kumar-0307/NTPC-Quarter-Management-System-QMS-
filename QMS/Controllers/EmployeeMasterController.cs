using QMS.Data;
using QMS.Data.Models;
using QMS.Models.DTOs;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace QMS.Controllers
{
    public class EmployeeMasterController : Controller
    {
        private readonly QMSDbContext _db = new QMSDbContext();
        private static readonly string[] QuarterTypes = { "A", "B", "C" };
        private static readonly string[] StatusOptions = { "Active", "Empty", "Retained", "Agency" };
        private static readonly string[] BloodGroups = { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };

        // GET: EmployeeMaster
        public ActionResult Index()
        {
            var employees = _db.EmployeeMasters.OrderBy(e => e.EmployeeName).ToList();
            return View(employees);
        }

        // GET: EmployeeMaster/Create
        [HttpGet]
        public ActionResult Create()
        {
            PopulateDropdowns();
            return View(new EmployeeMasterDto());
        }

        // POST: EmployeeMaster/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeMasterDto model)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropdowns();
                return View(model);
            }

            var employee = new EmployeeMaster
            {
                EmployeeName = model.EmployeeName,
                EmployeeNo = model.EmployeeNo,
                Department = model.Department,
                Designation = model.Designation,
                EmailId = model.EmailId,
                MobileNo = model.MobileNo,
                IntercomResidence = model.IntercomResidence,
                IntercomOffice = model.IntercomOffice,
                DateOfBirth = model.DateOfBirth,
                DateOfRetirement = model.DateOfBirth.AddYears(60),
                BloodGroup = model.BloodGroup,
                QuarterNo = model.QuarterNo,
                QuarterType = model.QuarterType,
                Status = model.Status,
                CreatedAt = DateTime.Now
            };

            if (model.Photo != null && model.Photo.ContentLength > 0)
            {
                var folder = Server.MapPath("~/Content/uploads/employeemaster");
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                var fileName = Guid.NewGuid() + Path.GetExtension(model.Photo.FileName);
                model.Photo.SaveAs(Path.Combine(folder, fileName));
                employee.PhotoPath = "/Content/uploads/employeemaster/" + fileName;
            }

            _db.EmployeeMasters.Add(employee);
            _db.SaveChanges();
            TempData["Success"] = "Employee added successfully.";
            return RedirectToAction("Index");
        }

        private void PopulateDropdowns()
        {
            ViewBag.QuarterTypes = new SelectList(QuarterTypes);
            ViewBag.StatusOptions = new SelectList(StatusOptions);
            ViewBag.BloodGroups = new SelectList(BloodGroups);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _db.Dispose();
            base.Dispose(disposing);
        }
    }
}