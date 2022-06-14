namespace TMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class landlordfix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Landlords", "LandlordDOB", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Landlords", "LandlordDOB", c => c.DateTime());
        }
    }
}
