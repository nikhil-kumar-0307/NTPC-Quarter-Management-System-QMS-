using System.Collections.Generic;
using QMS.Data.Models;

namespace QMS.Models.DTOs
{
    public class DashboardViewModel
    {
        public List<EmployeeMaster> Employees { get; set; }

        public List<string> Departments { get; set; }
        public List<string> Designations { get; set; }
        public List<string> BloodGroups { get; set; }

        public int TotalEmployees { get; set; }
        public int TotalUsers { get; set; }
        public int PendingRequests { get; set; }
        public int CompletedRequests { get; set; }
    }
}