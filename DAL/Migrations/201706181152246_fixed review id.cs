namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixedreviewid : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Reviews");
            DropColumn("dbo.Reviews", "Id");
            AddColumn("dbo.Reviews", "ReviewId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Reviews", "ReviewId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reviews", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Reviews");
            DropColumn("dbo.Reviews", "ReviewId");
            AddPrimaryKey("dbo.Reviews", "Id");
        }
    }
}
