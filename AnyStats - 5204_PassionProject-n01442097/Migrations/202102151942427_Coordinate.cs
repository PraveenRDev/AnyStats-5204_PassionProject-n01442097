namespace AnyStats___5204_PassionProject_n01442097.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Coordinate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Coordinates",
                c => new
                    {
                        CoordinateId = c.Int(nullable: false, identity: true),
                        XValue = c.String(),
                        YValue = c.String(),
                        Stat_StatId = c.Int(),
                    })
                .PrimaryKey(t => t.CoordinateId)
                .ForeignKey("dbo.Stats", t => t.Stat_StatId)
                .Index(t => t.Stat_StatId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Coordinates", "Stat_StatId", "dbo.Stats");
            DropIndex("dbo.Coordinates", new[] { "Stat_StatId" });
            DropTable("dbo.Coordinates");
        }
    }
}
