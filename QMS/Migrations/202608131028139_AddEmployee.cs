namespace QMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmployee : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        EmployeeNo = c.String(nullable: false, maxLength: 20),
                        QuarterNo = c.String(nullable: false, maxLength: 20),
                        QuarterType = c.String(nullable: false, maxLength: 5),
                        Status = c.String(nullable: false, maxLength: 20),
                        ProfilePicPath = c.String(),
                        ResidenceTelNo = c.String(maxLength: 15),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Employees");
        }
    }
}
