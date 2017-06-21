namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedintPricetodoublePrice : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Price", c => c.Double(nullable: false));
            AlterColumn("dbo.Orders", "OrderPrice", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "OrderPrice", c => c.Int(nullable: false));
            AlterColumn("dbo.Books", "Price", c => c.Int(nullable: false));
        }
    }
}
