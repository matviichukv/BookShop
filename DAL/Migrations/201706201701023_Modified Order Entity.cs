namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedOrderEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "DateOrdered", c => c.DateTime());
            DropColumn("dbo.Orders", "OrderNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "OrderNumber", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "DateOrdered");
        }
    }
}
