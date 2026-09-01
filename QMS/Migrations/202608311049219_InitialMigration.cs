namespace QMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AgencyName = c.String(nullable: false, maxLength: 100),
                        Contact = c.String(maxLength: 15),
                        PoNumber = c.String(nullable: false, maxLength: 20),
                        QuarterType = c.String(nullable: false, maxLength: 5),
                        QuarterNo = c.String(nullable: false, maxLength: 20),
                        MobileNo = c.String(maxLength: 15),
                        EmailId = c.String(maxLength: 100),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmployeeMaster",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeName = c.String(nullable: false, maxLength: 100),
                        EmployeeNo = c.String(nullable: false, maxLength: 20),
                        Department = c.String(nullable: false, maxLength: 50),
                        Designation = c.String(nullable: false, maxLength: 50),
                        Level = c.String(nullable: false, maxLength: 5),
                        EmailId = c.String(maxLength: 100),
                        MobileNo = c.String(maxLength: 15),
                        IntercomResidence = c.String(maxLength: 15),
                        IntercomOffice = c.String(maxLength: 15),
                        DateOfBirth = c.DateTime(nullable: false),
                        DateOfRetirement = c.DateTime(nullable: false),
                        BloodGroup = c.String(maxLength: 5),
                        PhotoPath = c.String(),
                        QuarterNo = c.String(nullable: false, maxLength: 20),
                        QuarterType = c.String(nullable: false, maxLength: 5),
                        Status = c.String(nullable: false, maxLength: 20),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeNumber = c.String(nullable: false, maxLength: 20),
                        PasswordHash = c.String(nullable: false),
                        Role = c.String(nullable: false, maxLength: 20),
                        LastLoginAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.EmployeeMaster");
            DropTable("dbo.Agencies");
        }
    }
}
