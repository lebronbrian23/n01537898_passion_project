namespace TMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullablfiled : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Properties", "PropertyConstructed", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Properties", "PropertyConstructed", c => c.DateTime());
        }
    }
}
