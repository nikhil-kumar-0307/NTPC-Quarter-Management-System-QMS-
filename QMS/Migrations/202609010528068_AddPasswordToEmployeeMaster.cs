namespace QMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPasswordToEmployeeMaster : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmployeeMaster", "Password", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmployeeMaster", "Password");
        }
    }
}
