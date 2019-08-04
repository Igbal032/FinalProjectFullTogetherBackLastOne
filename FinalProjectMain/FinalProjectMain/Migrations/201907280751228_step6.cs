namespace FinalProjectMain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class step6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Countries", "ZipCodeId", "dbo.ZipCodes");
            DropIndex("dbo.Countries", new[] { "ZipCodeId" });
            DropColumn("dbo.Countries", "ZipCodeId");
            DropTable("dbo.ZipCodes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ZipCodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        CreatedDate = c.DateTime(),
                        CreatedId = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedId = c.Int(),
                        DeletedDate = c.DateTime(),
                        DeletedId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Countries", "ZipCodeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Countries", "ZipCodeId");
            AddForeignKey("dbo.Countries", "ZipCodeId", "dbo.ZipCodes", "Id", cascadeDelete: true);
        }
    }
}
