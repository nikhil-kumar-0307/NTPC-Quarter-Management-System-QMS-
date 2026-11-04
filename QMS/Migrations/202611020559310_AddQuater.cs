namespace QMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQuater : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Quaters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuaterType = c.String(nullable: false, maxLength: 20),
                        QuaterNumber = c.String(nullable: false, maxLength: 20),
                        Status = c.String(nullable: false, maxLength: 20),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Quaters");
        }
    }
}
