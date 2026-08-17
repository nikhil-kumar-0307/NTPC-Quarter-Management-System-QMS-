namespace QMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmployeeMasterAndUpdateAgency : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeMaster",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeName = c.String(nullable: false, maxLength: 100),
                        EmployeeNo = c.String(nullable: false, maxLength: 20),
                        Department = c.String(nullable: false, maxLength: 50),
                        Designation = c.String(nullable: false, maxLength: 50),
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
            
            AddColumn("dbo.Agencies", "AgencyName", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Agencies", "Contact", c => c.String(maxLength: 15));
            AddColumn("dbo.Agencies", "PoNumber", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.Agencies", "MobileNo", c => c.String(maxLength: 15));
            AddColumn("dbo.Agencies", "EmailId", c => c.String(maxLength: 100));
            DropColumn("dbo.Agencies", "Name");
            DropColumn("dbo.Agencies", "EmployeeNo");
            DropColumn("dbo.Agencies", "Status");
            DropColumn("dbo.Agencies", "ProfilePicPath");
            DropColumn("dbo.Agencies", "ResidenceTelNo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Agencies", "ResidenceTelNo", c => c.String(maxLength: 15));
            AddColumn("dbo.Agencies", "ProfilePicPath", c => c.String());
            AddColumn("dbo.Agencies", "Status", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.Agencies", "EmployeeNo", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.Agencies", "Name", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.Agencies", "EmailId");
            DropColumn("dbo.Agencies", "MobileNo");
            DropColumn("dbo.Agencies", "PoNumber");
            DropColumn("dbo.Agencies", "Contact");
            DropColumn("dbo.Agencies", "AgencyName");
            DropTable("dbo.EmployeeMaster");
        }
    }
}
