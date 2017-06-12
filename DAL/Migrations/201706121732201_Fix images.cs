namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fiximages : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "PathToImageFile", c => c.String(nullable: false));
            DropColumn("dbo.Images", "ImageFile");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "ImageFile", c => c.Binary(nullable: false));
            DropColumn("dbo.Images", "PathToImageFile");
        }
    }
}
