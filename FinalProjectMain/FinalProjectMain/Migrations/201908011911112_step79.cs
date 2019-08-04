namespace FinalProjectMain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class step79 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ratings", "UserId", "dbo.Users");
            DropIndex("dbo.Ratings", new[] { "UserId" });
            DropColumn("dbo.Ratings", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ratings", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Ratings", "UserId");
            AddForeignKey("dbo.Ratings", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
