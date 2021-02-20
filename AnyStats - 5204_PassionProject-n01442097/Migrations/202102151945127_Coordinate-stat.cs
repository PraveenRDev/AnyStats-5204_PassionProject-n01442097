namespace AnyStats___5204_PassionProject_n01442097.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Coordinatestat : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Coordinates", "Stat_StatId", "dbo.Stats");
            DropIndex("dbo.Coordinates", new[] { "Stat_StatId" });
            RenameColumn(table: "dbo.Coordinates", name: "Stat_StatId", newName: "StatId");
            AlterColumn("dbo.Coordinates", "StatId", c => c.Int(nullable: false));
            CreateIndex("dbo.Coordinates", "StatId");
            AddForeignKey("dbo.Coordinates", "StatId", "dbo.Stats", "StatId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Coordinates", "StatId", "dbo.Stats");
            DropIndex("dbo.Coordinates", new[] { "StatId" });
            AlterColumn("dbo.Coordinates", "StatId", c => c.Int());
            RenameColumn(table: "dbo.Coordinates", name: "StatId", newName: "Stat_StatId");
            CreateIndex("dbo.Coordinates", "Stat_StatId");
            AddForeignKey("dbo.Coordinates", "Stat_StatId", "dbo.Stats", "StatId");
        }
    }
}
