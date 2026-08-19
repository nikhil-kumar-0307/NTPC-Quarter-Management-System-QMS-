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
                .OrderBy(e => e.EmployeeName)
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
                    .Distinct().OrderBy(b => b).ToList()
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
}