namespace TMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class leasemigrate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tenants", "PropertyId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tenants", "PropertyId");
        }
    }
}
