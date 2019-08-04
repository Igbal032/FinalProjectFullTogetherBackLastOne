namespace FinalProjectMain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class step2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        starCount = c.Int(nullable: false),
                        totalStarCount = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedId = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedId = c.Int(),
                        DeletedDate = c.DateTime(),
                        DeletedId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "UserId", "dbo.Users");
            DropForeignKey("dbo.Ratings", "ProductId", "dbo.Products");
            DropIndex("dbo.Ratings", new[] { "UserId" });
            DropIndex("dbo.Ratings", new[] { "ProductId" });
            DropTable("dbo.Ratings");
        }
    }
}
