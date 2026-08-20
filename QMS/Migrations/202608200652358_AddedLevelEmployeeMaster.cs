namespace QMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLevelEmployeeMaster : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmployeeMaster", "Level", c => c.String(nullable: false, maxLength: 5));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmployeeMaster", "Level");
        }
    }
}
