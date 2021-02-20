namespace AnyStats___5204_PassionProject_n01442097.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class doubleyValue : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Coordinates", "YValue", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Coordinates", "YValue", c => c.String());
        }
    }
}
