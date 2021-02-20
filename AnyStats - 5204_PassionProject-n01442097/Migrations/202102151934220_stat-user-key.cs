namespace AnyStats___5204_PassionProject_n01442097.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class statuserkey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stats", "AuthorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Stats", "AuthorId");
            AddForeignKey("dbo.Stats", "AuthorId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stats", "AuthorId", "dbo.AspNetUsers");
            DropIndex("dbo.Stats", new[] { "AuthorId" });
            DropColumn("dbo.Stats", "AuthorId");
        }
    }
}
