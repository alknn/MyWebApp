namespace MyWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPriceAndPhotoFieldsInProductTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "Photo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Photo");
            DropColumn("dbo.Products", "Price");
        }
    }
}
