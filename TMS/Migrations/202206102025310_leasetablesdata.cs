namespace TMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class leasetablesdata : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Leases", "Room", c => c.Int(nullable: false));
            AddColumn("dbo.Leases", "Floor", c => c.Int(nullable: false));
            AddColumn("dbo.Leases", "LeaseEndDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Tenants", "TenantRoom");
            DropColumn("dbo.Tenants", "TenantFloor");
            DropColumn("dbo.Tenants", "PropertyId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tenants", "PropertyId", c => c.Int(nullable: false));
            AddColumn("dbo.Tenants", "TenantFloor", c => c.Int(nullable: false));
            AddColumn("dbo.Tenants", "TenantRoom", c => c.Int(nullable: false));
            DropColumn("dbo.Leases", "LeaseEndDate");
            DropColumn("dbo.Leases", "Floor");
            DropColumn("dbo.Leases", "Room");
        }
    }
}
