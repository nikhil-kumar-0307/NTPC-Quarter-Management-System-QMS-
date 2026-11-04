namespace QMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQuaterLog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuaterLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuaterType = c.String(nullable: false, maxLength: 20),
                        QuaterNo = c.String(nullable: false, maxLength: 20),
                        AssignedTo = c.Int(nullable: false),
                        AssignedDate = c.DateTime(nullable: false),
                        RetainedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmployeeMaster", t => t.AssignedTo, cascadeDelete: true)
                .Index(t => t.AssignedTo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuaterLogs", "AssignedTo", "dbo.EmployeeMaster");
            DropIndex("dbo.QuaterLogs", new[] { "AssignedTo" });
            DropTable("dbo.QuaterLogs");
        }
    }
}
