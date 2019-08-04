namespace FinalProjectMain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class step80 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ratings", "currentStar", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ratings", "currentStar");
        }
    }
}
