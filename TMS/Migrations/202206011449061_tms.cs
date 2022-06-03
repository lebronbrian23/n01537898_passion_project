namespace TMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tms : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Landlords",
                c => new
                    {
                        LandlordId = c.Int(nullable: false, identity: true),
                        LandlordName = c.String(),
                        LandlordDOB = c.DateTime(),
                        LandlordPhone = c.String(),
                        LandlordEmail = c.String(),
                    })
                .PrimaryKey(t => t.LandlordId);
            
            CreateTable(
                "dbo.Properties",
                c => new
                    {
                        PropertyId = c.Int(nullable: false, identity: true),
                        PropertyAddress = c.String(),
                        PropertyFloors = c.String(),
                        PropertyConstructed = c.DateTime(),
                        LandlordId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PropertyId)
                .ForeignKey("dbo.Landlords", t => t.LandlordId, cascadeDelete: true)
                .Index(t => t.LandlordId);
            
            CreateTable(
                "dbo.Leases",
                c => new
                    {
                        LeaseId = c.Int(nullable: false, identity: true),
                        room = c.Int(nullable: false),
                        floor = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                        PropertyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LeaseId)
                .ForeignKey("dbo.Properties", t => t.PropertyId, cascadeDelete: true)
                .ForeignKey("dbo.Tenants", t => t.TenantId, cascadeDelete: true)
                .Index(t => t.TenantId)
                .Index(t => t.PropertyId);
            
            CreateTable(
                "dbo.Tenants",
                c => new
                    {
                        TenantId = c.Int(nullable: false, identity: true),
                        TenantName = c.String(),
                        TenantEmail = c.String(),
                        TenantPhone = c.String(),
                        TenantEmergencyContact = c.String(),
                        TenantJoined = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TenantId);
            
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
