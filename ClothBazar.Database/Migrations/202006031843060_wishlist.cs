namespace ClothBazar.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wishlist : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WishLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserID = c.String(),
                        AddedOn = c.DateTime(nullable: false),
                        ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WishLists", "ProductID", "dbo.Products");
            DropIndex("dbo.WishLists", new[] { "ProductID" });
            DropTable("dbo.WishLists");
        }
    }
}
