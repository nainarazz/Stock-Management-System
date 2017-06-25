namespace StockSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAnnotationToProductModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "ItemName", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Products", "ItemUnit", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "ShelfLocation", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "ShelfLocation", c => c.String());
            AlterColumn("dbo.Products", "ItemUnit", c => c.String());
            AlterColumn("dbo.Products", "ItemName", c => c.String());
        }
    }
}
