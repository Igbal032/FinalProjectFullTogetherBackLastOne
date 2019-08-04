namespace FinalProjectMain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class step78 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ratings", "starCount", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "totalStarLevel", c => c.Int(nullable: false));
            DropColumn("dbo.Ratings", "starLevel");
            DropColumn("dbo.Ratings", "totalStarCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ratings", "totalStarCount", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "starLevel", c => c.Int(nullable: false));
            DropColumn("dbo.Ratings", "totalStarLevel");
            DropColumn("dbo.Ratings", "starCount");
        }
    }
}
