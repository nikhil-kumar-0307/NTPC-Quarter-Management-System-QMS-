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
                TotalUsers = 0,        
                PendingRequests = 0,  
                CompletedRequests = 0
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