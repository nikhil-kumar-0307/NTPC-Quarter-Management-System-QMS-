namespace QMS.Migrations
{
    using QMS.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration
        : DbMigrationsConfiguration<QMS.Data.QMSDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(QMS.Data.QMSDbContext context)
        {
            // Check if Admin user already exists
            if (!context.Users.Any(u => u.EmployeeNumber == "ADMIN001"))
            {
                context.Users.Add(
                    new User
                    {
                        EmployeeNumber = "ADMIN001",
                        PasswordHash = "Admin@123",
                        Role = "Admin",
                        LastLoginAt = null
                    }
                );

                context.SaveChanges();
            }
        }
    }
}