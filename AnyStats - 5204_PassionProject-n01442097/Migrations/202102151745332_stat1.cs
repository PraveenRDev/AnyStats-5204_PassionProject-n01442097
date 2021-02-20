namespace AnyStats___5204_PassionProject_n01442097.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stat1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Stats",
                c => new
                    {
                        StatId = c.Int(nullable: false, identity: true),
                        StatName = c.String(),
                        StatDescription = c.String(),
                        XAxis = c.String(),
                        YAxis = c.String(),
                        isPublic = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.StatId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Stats");
        }
    }
}
