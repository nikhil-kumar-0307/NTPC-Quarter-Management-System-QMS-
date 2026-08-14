using System.Web.Mvc;

namespace QMS.Controllers
{
    public class DashboardController : Controller
    {
        [HttpGet]
        public ActionResult AdminDashboard()
        {
            return View();
        }
    }
}