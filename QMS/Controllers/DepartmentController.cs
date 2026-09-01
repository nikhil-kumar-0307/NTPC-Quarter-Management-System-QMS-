using QMS.Data;
using QMS.Models;
using QMS.Models.DTOs;
using System.Linq;
using System.Web.Mvc;

namespace QMS.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly QMSDbContext db = new QMSDbContext();

        // GET: Department
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "Department";

            var departments = db.Departments
                .OrderBy(d => d.Name)
                .Select(d => new DepartmentDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Code = d.Code,
                    IsActive = d.IsActive
                })
                .ToList();

            return View(departments);
        }

        // GET: Department/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.ActiveMenu = "Department";

            var department = db.Departments.Find(id);
            if (department == null)
                return HttpNotFound();

            var dto = new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                IsActive = department.IsActive
            };

            return View(dto);
        }

        // GET: Department/Create
        public ActionResult Create()
        {
            ViewBag.ActiveMenu = "Department";
            return View(new DepartmentDto { IsActive = true });
        }

        // POST: Department/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DepartmentDto dto)
        {
            ViewBag.ActiveMenu = "Department";

            if (!ModelState.IsValid)
                return View(dto);

            var department = new Department
            {
                Name = dto.Name,
                Code = dto.Code,
                IsActive = dto.IsActive
            };

            db.Departments.Add(department);
            db.SaveChanges();

            TempData["SuccessMessage"] = "Department created successfully.";
            return RedirectToAction("Index");
        }

        // GET: Department/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.ActiveMenu = "Department";

            var department = db.Departments.Find(id);
            if (department == null)
                return HttpNotFound();

            var dto = new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                IsActive = department.IsActive
            };

            return View(dto);
        }

        // POST: Department/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DepartmentDto dto)
        {
            ViewBag.ActiveMenu = "Department";

            if (!ModelState.IsValid)
                return View(dto);

            var department = db.Departments.Find(dto.Id);
            if (department == null)
                return HttpNotFound();

            department.Name = dto.Name;
            department.Code = dto.Code;
            department.IsActive = dto.IsActive;

            db.SaveChanges();

            TempData["SuccessMessage"] = "Department updated successfully.";
            return RedirectToAction("Index");
        }

        // GET: Department/Delete/5
        public ActionResult Delete(int id)
        {
            ViewBag.ActiveMenu = "Department";

            var department = db.Departments.Find(id);
            if (department == null)
                return HttpNotFound();

            var dto = new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                IsActive = department.IsActive
            };

            return View(dto);
        }

        // POST: Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var department = db.Departments.Find(id);
            if (department != null)
            {
                db.Departments.Remove(department);
                db.SaveChanges();
            }

            TempData["SuccessMessage"] = "Department deleted successfully.";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}