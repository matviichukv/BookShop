namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeddoublePricetodecimalPrice : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Orders", "OrderPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "OrderPrice", c => c.Double(nullable: false));
            AlterColumn("dbo.Books", "Price", c => c.Double(nullable: false));
        }
    }
}
