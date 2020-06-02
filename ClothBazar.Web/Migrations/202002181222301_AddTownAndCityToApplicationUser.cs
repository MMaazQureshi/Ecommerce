namespace ClothBazar.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTownAndCityToApplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "City", c => c.String(maxLength: 20));
            AddColumn("dbo.AspNetUsers", "Town", c => c.String(maxLength: 25));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Town");
            DropColumn("dbo.AspNetUsers", "City");
        }
    }
}
