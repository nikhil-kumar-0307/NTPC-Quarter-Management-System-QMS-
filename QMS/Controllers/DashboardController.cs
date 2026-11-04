using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using QMS.Data;
using QMS.Models.DTOs;

namespace QMS.Controllers
{
    public class DashboardController : Controller
    {
        private readonly QMSDbContext _db = new QMSDbContext();

        private static readonly string[] QuarterStatuses = { "Active", "Retained", "Occupied", "Vacant" };

        [HttpGet]
        public ActionResult AdminDashboard()
        {
            var today = DateTime.Today;
            var retireLimit = today.AddDays(180);

            var model = new DashboardViewModel
            {
                TotalEmployees = _db.EmployeeMasters.Count(),
                TotalQuarters = _db.Quaters.Count(),
                TotalDepartments = _db.EmployeeMasters
                                      .Select(e => e.Department)
                                      .Distinct()
                                      .Count()
            };

            // Quarter status breakdown (all four statuses always present)
            var statusCounts = _db.Quaters
                                  .GroupBy(q => q.Status)
                                  .Select(g => new { Status = g.Key, Count = g.Count() })
                                  .ToList();

            foreach (var s in QuarterStatuses)
            {
                var match = statusCounts.FirstOrDefault(x =>
                    string.Equals(x.Status, s, StringComparison.OrdinalIgnoreCase));
                model.QuarterStatusCounts[s] = match != null ? match.Count : 0;
            }

            model.OccupiedQuarters = model.QuarterStatusCounts["Occupied"];
            model.VacantQuarters = model.QuarterStatusCounts["Vacant"];

            // Top 6 departments by headcount
            model.DepartmentStats = _db.EmployeeMasters
                .GroupBy(e => e.Department)
                .Select(g => new { Label = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(6)
                .ToList()
                .Select(x => new DashboardStat { Label = x.Label, Count = x.Count })
                .ToList();

            // Retiring within 180 days
            model.RetiringSoon = _db.EmployeeMasters
                .Where(e => e.DateOfRetirement >= today && e.DateOfRetirement <= retireLimit)
                .OrderBy(e => e.DateOfRetirement)
                .Take(5)
                .Select(e => new
                {
                    e.EmployeeName,
                    e.EmployeeNo,
                    e.Department,
                    e.DateOfRetirement
                })
                .ToList()
                .Select(e => new RetiringEmployee
                {
                    Name = e.EmployeeName,
                    EmployeeNo = e.EmployeeNo,
                    Department = e.Department,
                    RetirementDate = e.DateOfRetirement
                })
                .ToList();

            // Latest 6 quarter log entries
            model.RecentLogs = _db.QuaterLogs
                .Include(l => l.Employee)
                .OrderByDescending(l => l.AssignedDate)
                .ThenByDescending(l => l.Id)
                .Take(6)
                .ToList();

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _db.Dispose();
            base.Dispose(disposing);
        }
    }
}