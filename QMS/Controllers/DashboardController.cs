using System.Linq;
using System.Web.Mvc;
using QMS.Data;
using QMS.Models.DTOs;

namespace QMS.Controllers
{
    public class DashboardController : Controller
    {
        private readonly QMSDbContext _db = new QMSDbContext();

        [HttpGet]
        public ActionResult AdminDashboard()
        {
            var model = new DashboardViewModel
            {
                TotalEmployees = _db.EmployeeMasters.Count(),
                TotalUsers = 0,        // TODO: wire to Users table
                PendingRequests = 0,   // TODO: wire to Requests table
                CompletedRequests = 0  // TODO: wire to Requests table
            };
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _db.Dispose();
            base.Dispose(disposing);
        }
    }
}