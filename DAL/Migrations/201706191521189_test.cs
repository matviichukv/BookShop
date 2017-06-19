namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Test", c => c.Int(nullable:true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "Test");
        }
    }
}
