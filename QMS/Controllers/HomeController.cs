using System.Linq;
using System.Web.Mvc;
using QMS.Data;
using QMS.Models.DTOs;

namespace QMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly QMSDbContext _db = new QMSDbContext();

        public ActionResult Index()
        {
            var employees = _db.EmployeeMasters
                .ToList()
                .OrderBy(e => e.Level, new LevelComparer())
                .ThenBy(e => e.EmployeeName)
                .ToList();

            var model = new DashboardViewModel
            {
                Employees = employees,
                Departments = employees.Select(e => e.Department)
                    .Where(d => !string.IsNullOrWhiteSpace(d))
                    .Distinct().OrderBy(d => d).ToList(),
                Designations = employees.Select(e => e.Designation)
                    .Where(d => !string.IsNullOrWhiteSpace(d))
                    .Distinct().OrderBy(d => d).ToList(),
                BloodGroups = employees.Select(e => e.BloodGroup)
                    .Where(b => !string.IsNullOrWhiteSpace(b))
                    .Distinct().OrderBy(b => b).ToList(),
                Levels = employees.Select(e => e.Level)
                    .Where(l => !string.IsNullOrWhiteSpace(l))
                    .Distinct()
                    .OrderBy(l => l, new LevelComparer())
                    .ToList()
            };

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _db.Dispose();
            base.Dispose(disposing);
        }
    }

    
    public class LevelComparer : System.Collections.Generic.IComparer<string>
    {
        public int Compare(string x, string y)
        {
            var (catX, numX) = Parse(x);
            var (catY, numY) = Parse(y);

            int catCompare = catX.CompareTo(catY);
            if (catCompare != 0) return catCompare;

            return numX.CompareTo(numY);
        }

        private (int category, int number) Parse(string level)
        {
            if (string.IsNullOrWhiteSpace(level))
                return (2, 0); // blanks/unknowns sort last

            char prefix = char.ToUpperInvariant(level[0]);
            int category = prefix == 'E' ? 0 : (prefix == 'W' ? 1 : 2); // E > W in priority

            int number = 0;
            if (level.Length > 1)
                int.TryParse(level.Substring(1), out number);

            return (category, number);
        }
    }
}