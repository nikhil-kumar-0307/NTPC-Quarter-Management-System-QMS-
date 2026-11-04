using System;
using System.Collections.Generic;
using QMS.Data.Models;
using QMS.Models;

namespace QMS.Models.DTOs
{
    public class DashboardViewModel
    {
        public List<EmployeeMaster> Employees { get; set; }

        public List<string> Departments { get; set; }
        public List<string> Designations { get; set; }
        public List<string> BloodGroups { get; set; }
        public List<string> Levels { get; set; }

        public int TotalEmployees { get; set; }
        public int TotalUsers { get; set; }
        public int PendingRequests { get; set; }
        public int CompletedRequests { get; set; }

        // ---- New ----
        public int TotalQuarters { get; set; }
        public int OccupiedQuarters { get; set; }
        public int VacantQuarters { get; set; }
        public int TotalDepartments { get; set; }

        // Quarter status -> count (Active, Retained, Occupied, Vacant)
        public Dictionary<string, int> QuarterStatusCounts { get; set; } = new Dictionary<string, int>();

        // Top departments by employee count
        public List<DashboardStat> DepartmentStats { get; set; } = new List<DashboardStat>();

        // Employees retiring in the next 180 days
        public List<RetiringEmployee> RetiringSoon { get; set; } = new List<RetiringEmployee>();

        // Latest quarter log entries
        public List<QuaterLog> RecentLogs { get; set; } = new List<QuaterLog>();
    }

    public class DashboardStat
    {
        public string Label { get; set; }
        public int Count { get; set; }
    }

    public class RetiringEmployee
    {
        public string Name { get; set; }
        public string EmployeeNo { get; set; }
        public string Department { get; set; }
        public DateTime RetirementDate { get; set; }
    }
}