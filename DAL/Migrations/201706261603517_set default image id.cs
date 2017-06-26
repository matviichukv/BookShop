namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setdefaultimageid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Authors", "ImageId", "dbo.Images");
            DropForeignKey("dbo.Books", "ImageId", "dbo.Images");
            DropIndex("dbo.Authors", new[] { "ImageId" });
            DropIndex("dbo.Books", new[] { "ImageId" });
            AlterColumn("dbo.Authors", "ImageId", c => c.Int(nullable: false, defaultValue:44));
            AlterColumn("dbo.Books", "ImageId", c => c.Int(nullable: false, defaultValue:44));
            CreateIndex("dbo.Authors", "ImageId");
            CreateIndex("dbo.Books", "ImageId");
            AddForeignKey("dbo.Authors", "ImageId", "dbo.Images", "ImageId", cascadeDelete: false);
            AddForeignKey("dbo.Books", "ImageId", "dbo.Images", "ImageId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "ImageId", "dbo.Images");
            DropForeignKey("dbo.Authors", "ImageId", "dbo.Images");
            DropIndex("dbo.Books", new[] { "ImageId" });
            DropIndex("dbo.Authors", new[] { "ImageId" });
            AlterColumn("dbo.Books", "ImageId", c => c.Int());
            AlterColumn("dbo.Authors", "ImageId", c => c.Int());
            CreateIndex("dbo.Books", "ImageId");
            CreateIndex("dbo.Authors", "ImageId");
            AddForeignKey("dbo.Books", "ImageId", "dbo.Images", "ImageId");
            AddForeignKey("dbo.Authors", "ImageId", "dbo.Images", "ImageId");
        }
    }
}
