using QMS.Data;
using QMS.Models;
using QMS.Models.DTOs;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace QMS.Controllers
{
    public class QuaterLogController : Controller
    {
        private readonly QMSDbContext _db = new QMSDbContext();

        private static readonly string[] QuarterTypes = { "A", "B", "C" };

        // GET: QuaterLog
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "QuaterLog";
            var logs = _db.QuaterLogs
                          .Include(l => l.Employee)
                          .OrderByDescending(l => l.AssignedDate)
                          .ThenByDescending(l => l.Id)
                          .ToList();
            return View(logs);
        }

        // GET: QuaterLog/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ActiveMenu = "QuaterLog";
            PopulateDropdowns(null, null);
            return View(new QuaterLogDto());
        }

        // POST: QuaterLog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuaterLogDto model)
        {
            ViewBag.ActiveMenu = "QuaterLog";
            ValidateLog(model);

            if (!ModelState.IsValid)
            {
                PopulateDropdowns(model.QuaterType, model.QuaterNo);
                return View(model);
            }

            _db.QuaterLogs.Add(new QuaterLog
            {
                QuaterType = model.QuaterType,
                QuaterNo = model.QuaterNo,
                AssignedTo = model.AssignedTo.Value,
                AssignedDate = model.AssignedDate.Value,
                RetainedDate = model.RetainedDate
            });
            _db.SaveChanges();

            TempData["Success"] = "Quarter log added successfully.";
            return RedirectToAction("Index");
        }

        // GET: QuaterLog/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            ViewBag.ActiveMenu = "QuaterLog";

            if (id == null) return HttpNotFound();

            var log = _db.QuaterLogs.Find(id.Value);
            if (log == null) return HttpNotFound();

            var model = new QuaterLogDto
            {
                Id = log.Id,
                QuaterType = log.QuaterType,
                QuaterNo = log.QuaterNo,
                AssignedTo = log.AssignedTo,
                AssignedDate = log.AssignedDate,
                RetainedDate = log.RetainedDate
            };

            PopulateDropdowns(model.QuaterType, model.QuaterNo);
            return View(model);
        }

        // POST: QuaterLog/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuaterLogDto model)
        {
            ViewBag.ActiveMenu = "QuaterLog";
            ValidateLog(model);

            if (!ModelState.IsValid)
            {
                PopulateDropdowns(model.QuaterType, model.QuaterNo);
                return View(model);
            }

            var log = _db.QuaterLogs.Find(model.Id);
            if (log == null) return HttpNotFound();

            log.QuaterType = model.QuaterType;
            log.QuaterNo = model.QuaterNo;
            log.AssignedTo = model.AssignedTo.Value;
            log.AssignedDate = model.AssignedDate.Value;
            log.RetainedDate = model.RetainedDate;

            _db.SaveChanges();

            TempData["Success"] = "Quarter log updated successfully.";
            return RedirectToAction("Index");
        }

        // POST: QuaterLog/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var log = _db.QuaterLogs.Find(id);
            if (log == null)
            {
                TempData["Error"] = "Quarter log not found.";
                return RedirectToAction("Index");
            }

            _db.QuaterLogs.Remove(log);
            _db.SaveChanges();

            TempData["Success"] = "Quarter log deleted successfully.";
            return RedirectToAction("Index");
        }

        // GET: QuaterLog/GetQuaterNumbers?type=A  (used by the cascading dropdown)
        [HttpGet]
        public JsonResult GetQuaterNumbers(string type)
        {
            var numbers = _db.Quaters
                             .Where(q => q.IsActive && q.QuaterType == type)
                             .OrderBy(q => q.QuaterNumber)
                             .Select(q => q.QuaterNumber)
                             .ToList();

            return Json(numbers, JsonRequestBehavior.AllowGet);
        }

        private void ValidateLog(QuaterLogDto model)
        {
            if (model.AssignedDate.HasValue && model.RetainedDate.HasValue
                && model.RetainedDate.Value < model.AssignedDate.Value)
            {
                ModelState.AddModelError("RetainedDate", "Retained date cannot be earlier than the assigned date.");
            }

            if (!string.IsNullOrWhiteSpace(model.QuaterType) && !string.IsNullOrWhiteSpace(model.QuaterNo))
            {
                bool exists = _db.Quaters.Any(q => q.QuaterType == model.QuaterType
                                                && q.QuaterNumber == model.QuaterNo);
                if (!exists)
                    ModelState.AddModelError("QuaterNo", "This quarter does not exist in the Quarters list.");
            }
        }

        private void PopulateDropdowns(string selectedType, string selectedNo)
        {
            ViewBag.QuarterTypes = new SelectList(QuarterTypes);

            // Employees: value = Id, text = "Name (EmpNo)"
            var employees = _db.EmployeeMasters
                               .OrderBy(e => e.EmployeeName)
                               .Select(e => new { e.Id, e.EmployeeName, e.EmployeeNo })
                               .ToList()
                               .Select(e => new SelectListItem
                               {
                                   Value = e.Id.ToString(),
                                   Text = e.EmployeeName + " (" + e.EmployeeNo + ")"
                               })
                               .ToList();

            ViewBag.Employees = new SelectList(employees, "Value", "Text");

            // All active quarters grouped by type: { "A": ["1","2"], "B": [...] }
            var quarters = _db.Quaters
                              .Where(q => q.IsActive)
                              .Select(q => new { q.QuaterType, q.QuaterNumber })
                              .ToList();

            var map = quarters
                .GroupBy(q => (q.QuaterType ?? "").Trim().ToUpper())
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.QuaterNumber)
                          .OrderBy(n => n.Length)
                          .ThenBy(n => n)
                          .ToList());

            ViewBag.QuaterMapJson = new JavaScriptSerializer().Serialize(map);

            // Initial list for the selected type (used on Edit / after a validation error)
            var numbers = string.IsNullOrEmpty(selectedType)
                ? new string[0]
                : _db.Quaters
                     .Where(q => q.IsActive && q.QuaterType == selectedType)
                     .OrderBy(q => q.QuaterNumber)
                     .Select(q => q.QuaterNumber)
                     .ToArray();

            ViewBag.QuaterNumbers = new SelectList(numbers, selectedNo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _db.Dispose();
            base.Dispose(disposing);
        }
    }
}