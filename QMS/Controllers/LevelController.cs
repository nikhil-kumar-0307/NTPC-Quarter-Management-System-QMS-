using QMS.Data;
using QMS.Models;
using QMS.Models.DTOs;
using System.Linq;
using System.Web.Mvc;

namespace QMS.Controllers
{
    public class LevelController : Controller
    {
        private readonly QMSDbContext db = new QMSDbContext();

        // GET: Level
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "Level";

            var levels = db.Levels
                .OrderBy(l => l.Name)
                .Select(l => new LevelDto
                {
                    Id = l.Id,
                    Name = l.Name,
                    IsActive = l.IsActive
                })
                .ToList();

            return View(levels);
        }

        // GET: Level/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.ActiveMenu = "Level";

            var level = db.Levels.Find(id);
            if (level == null)
                return HttpNotFound();

            var dto = new LevelDto
            {
                Id = level.Id,
                Name = level.Name,
                IsActive = level.IsActive
            };

            return View(dto);
        }

        // GET: Level/Create
        public ActionResult Create()
        {
            ViewBag.ActiveMenu = "Level";
            return View(new LevelDto { IsActive = true });
        }

        // POST: Level/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LevelDto dto)
        {
            ViewBag.ActiveMenu = "Level";

            if (!ModelState.IsValid)
                return View(dto);

            var level = new Level
            {
                Name = dto.Name,
                IsActive = dto.IsActive
            };

            db.Levels.Add(level);
            db.SaveChanges();

            TempData["SuccessMessage"] = "Level created successfully.";
            return RedirectToAction("Index");
        }

        // GET: Level/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.ActiveMenu = "Level";

            var level = db.Levels.Find(id);
            if (level == null)
                return HttpNotFound();

            var dto = new LevelDto
            {
                Id = level.Id,
                Name = level.Name,
                IsActive = level.IsActive
            };

            return View(dto);
        }

        // POST: Level/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LevelDto dto)
        {
            ViewBag.ActiveMenu = "Level";

            if (!ModelState.IsValid)
                return View(dto);

            var level = db.Levels.Find(dto.Id);
            if (level == null)
                return HttpNotFound();

            level.Name = dto.Name;
            level.IsActive = dto.IsActive;

            db.SaveChanges();

            TempData["SuccessMessage"] = "Level updated successfully.";
            return RedirectToAction("Index");
        }

        // GET: Level/Delete/5
        public ActionResult Delete(int id)
        {
            ViewBag.ActiveMenu = "Level";

            var level = db.Levels.Find(id);
            if (level == null)
                return HttpNotFound();

            var dto = new LevelDto
            {
                Id = level.Id,
                Name = level.Name,
                IsActive = level.IsActive
            };

            return View(dto);
        }

        // POST: Level/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var level = db.Levels.Find(id);
            if (level != null)
            {
                db.Levels.Remove(level);
                db.SaveChanges();
            }

            TempData["SuccessMessage"] = "Level deleted successfully.";
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