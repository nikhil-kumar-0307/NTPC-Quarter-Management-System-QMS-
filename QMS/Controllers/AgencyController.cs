using QMS.Data;
using QMS.Data.Models;
using QMS.Models.DTOs;
using System;
using System.Linq;
using System.Web.Mvc;

namespace QMS.Controllers
{
    public class AgencyController : Controller
    {
        private readonly QMSDbContext _db = new QMSDbContext();
        private static readonly string[] QuarterTypes = { "A", "B", "C" };

        // GET: Agency
        public ActionResult Index()
        {
            var agencies = _db.Agencies.OrderBy(a => a.AgencyName).ToList();
            return View(agencies);
        }

        // GET: Agency/Create
        [HttpGet]
        public ActionResult Create()
        {
            PopulateDropdowns();
            return View(new AgencyDto());
        }

        // POST: Agency/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AgencyDto model)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropdowns();
                return View(model);
            }

            var agency = new Agency
            {
                AgencyName = model.AgencyName,
                Contact = model.Contact,
                PoNumber = model.PoNumber,
                QuarterType = model.QuarterType,
                QuarterNo = model.QuarterNo,
                MobileNo = model.MobileNo,
                EmailId = model.EmailId,
                CreatedAt = DateTime.Now
            };

            _db.Agencies.Add(agency);
            _db.SaveChanges();
            TempData["Success"] = "Agency added successfully.";
            return RedirectToAction("Index");
        }

        private void PopulateDropdowns()
        {
            ViewBag.QuarterTypes = new SelectList(QuarterTypes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _db.Dispose();
            base.Dispose(disposing);
        }
    }
}