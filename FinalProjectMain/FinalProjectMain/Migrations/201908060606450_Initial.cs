namespace FinalProjectMain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.siteInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Phone = c.String(),
                        Email = c.String(),
                        Instgram = c.String(),
                        CreatedDate = c.DateTime(),
                        CreatedId = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedId = c.Int(),
                        DeletedDate = c.DateTime(),
                        DeletedId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.siteInfoes");
        }
    }
}
