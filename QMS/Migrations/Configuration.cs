namespace QMS.Migrations
{
    using QMS.Models;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<QMS.Data.QMSDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(QMS.Data.QMSDbContext context)
        {
            context.Users.AddOrUpdate(
                u => u.EmployeeNumber,
                new User
                {
                    EmployeeNumber = "EMP001",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                    Role = "Admin",
                    LastLoginAt = null
                },
                new User
                {
                    EmployeeNumber = "EMP002",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Staff@123"),
                    Role = "Staff",
                    LastLoginAt = null
                }
            );
        }
    }
}