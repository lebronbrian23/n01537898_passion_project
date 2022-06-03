namespace TMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class propertyname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Properties", "PropertyName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Properties", "PropertyName");
        }
    }
}
