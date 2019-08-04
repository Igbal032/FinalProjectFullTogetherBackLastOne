namespace FinalProjectMain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class step : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ratings", "starLevel", c => c.Int(nullable: false));
            DropColumn("dbo.Ratings", "starCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ratings", "starCount", c => c.Int(nullable: false));
            DropColumn("dbo.Ratings", "starLevel");
        }
    }
}
