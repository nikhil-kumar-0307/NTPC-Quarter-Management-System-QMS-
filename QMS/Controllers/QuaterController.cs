using QMS.Data;
using QMS.Models;
using QMS.Models.DTOs;
using System.Linq;
using System.Web.Mvc;

namespace QMS.Controllers
{
    public class QuaterController : Controller
    {
        private readonly QMSDbContext _db = new QMSDbContext();

        private static readonly string[] QuarterTypes = { "A", "B", "C" };
        private static readonly string[] StatusOptions = { "Active", "Retained", "Occupied", "Vacant" };

        // GET: Quater
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "Quater";
            var list = _db.Quaters
                          .OrderBy(q => q.QuaterType)
                          .ThenBy(q => q.QuaterNumber)
                          .ToList();
            return View(list);
        }

        // GET: Quater/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ActiveMenu = "Quater";
            PopulateDropdowns();
            return View(new QuaterDto());
        }

        // POST: Quater/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuaterDto model)
        {
            ViewBag.ActiveMenu = "Quater";

            if (IsDuplicate(model.QuaterType, model.QuaterNumber, null))
                ModelState.AddModelError("QuaterNumber", "This quarter already exists for the selected type.");

            if (!ModelState.IsValid)
            {
                PopulateDropdowns();
                return View(model);
            }

            _db.Quaters.Add(new Quater
            {
                QuaterType = model.QuaterType,
                QuaterNumber = model.QuaterNumber.Trim(),
                Status = model.Status,
                IsActive = model.IsActive
            });
            _db.SaveChanges();

            TempData["Success"] = "Quarter added successfully.";
            return RedirectToAction("Index");
        }

        // GET: Quater/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            ViewBag.ActiveMenu = "Quater";

            if (id == null) return HttpNotFound();

            var q = _db.Quaters.Find(id.Value);
            if (q == null) return HttpNotFound();

            var model = new QuaterDto
            {
                Id = q.Id,
                QuaterType = q.QuaterType,
                QuaterNumber = q.QuaterNumber,
                Status = q.Status,
                IsActive = q.IsActive
            };

            PopulateDropdowns();
            return View(model);
        }

        // POST: Quater/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuaterDto model)
        {
            ViewBag.ActiveMenu = "Quater";

            if (IsDuplicate(model.QuaterType, model.QuaterNumber, model.Id))
                ModelState.AddModelError("QuaterNumber", "This quarter already exists for the selected type.");

            if (!ModelState.IsValid)
            {
                PopulateDropdowns();
                return View(model);
            }

            var q = _db.Quaters.Find(model.Id);
            if (q == null) return HttpNotFound();

            q.QuaterType = model.QuaterType;
            q.QuaterNumber = model.QuaterNumber.Trim();
            q.Status = model.Status;
            q.IsActive = model.IsActive;

            _db.SaveChanges();

            TempData["Success"] = "Quarter updated successfully.";
            return RedirectToAction("Index");
        }

        // POST: Quater/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var q = _db.Quaters.Find(id);
            if (q == null)
            {
                TempData["Error"] = "Quarter not found.";
                return RedirectToAction("Index");
            }

            _db.Quaters.Remove(q);
            _db.SaveChanges();

            TempData["Success"] = $"Quarter '{q.QuaterType}/{q.QuaterNumber}' deleted successfully.";
            return RedirectToAction("Index");
        }

        private bool IsDuplicate(string type, string number, int? excludeId)
        {
            if (string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(number))
                return false;

            var n = number.Trim();
            return _db.Quaters.Any(q => q.QuaterType == type
                                     && q.QuaterNumber == n
                                     && (excludeId == null || q.Id != excludeId.Value));
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