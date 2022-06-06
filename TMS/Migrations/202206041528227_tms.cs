namespace TMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tms : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Leases",
                c => new
                {
                    LeaseId = c.Int(nullable: false, identity: true),
                    TenantId = c.Int(nullable: false),
                    PropertyId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.LeaseId)
                .ForeignKey("dbo.Properties", t => t.PropertyId, cascadeDelete: true)
                .ForeignKey("dbo.Tenants", t => t.TenantId, cascadeDelete: true)
                .Index(t => t.TenantId)
                .Index(t => t.PropertyId);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leases", "TenantId", "dbo.Tenants");
            DropForeignKey("dbo.Leases", "PropertyId", "dbo.Properties");
            DropForeignKey("dbo.Properties", "LandlordId", "dbo.Landlords");
            DropIndex("dbo.Leases", new[] { "PropertyId" });
            DropIndex("dbo.Leases", new[] { "TenantId" });
            DropIndex("dbo.Properties", new[] { "LandlordId" });
            DropTable("dbo.Tenants");
            DropTable("dbo.Leases");
            DropTable("dbo.Properties");
            DropTable("dbo.Landlords");
        }
    }
}
