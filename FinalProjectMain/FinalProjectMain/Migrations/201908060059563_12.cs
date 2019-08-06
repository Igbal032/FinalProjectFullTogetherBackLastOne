namespace FinalProjectMain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Notification", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Notification", c => c.Boolean(nullable: false));
        }
    }
}
