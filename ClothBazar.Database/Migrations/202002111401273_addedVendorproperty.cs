namespace ClothBazar.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedVendorproperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Vendor", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Vendor");
        }
    }
}
