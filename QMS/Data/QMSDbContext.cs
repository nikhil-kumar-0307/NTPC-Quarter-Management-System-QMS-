using QMS.Data.Models;
using QMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QMS.Data
{
    public class QMSDbContext : DbContext
    {
        public QMSDbContext() : base("QMSDbContext") { }

        public DbSet <User> Users { get; set; }
        //public DbSet<Employee> Employees { get; set; }
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<EmployeeMaster> EmployeeMasters { get; set; }
    }
}