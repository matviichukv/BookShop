namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Madeimageidnullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Authors", "ImageId", "dbo.Images");
            DropForeignKey("dbo.Books", "ImageId", "dbo.Images");
            DropForeignKey("dbo.Users", "ImageId", "dbo.Images");
            DropIndex("dbo.Authors", new[] { "ImageId" });
            DropIndex("dbo.Books", new[] { "ImageId" });
            DropIndex("dbo.Users", new[] { "ImageId" });
            AddColumn("dbo.Users", "Salt", c => c.String(nullable: false));
            AlterColumn("dbo.Authors", "ImageId", c => c.Int(nullable: true));
            AlterColumn("dbo.Books", "ImageId", c => c.Int(nullable: true));
            AlterColumn("dbo.Users", "ImageId", c => c.Int(nullable:true));
            CreateIndex("dbo.Authors", "ImageId");
            CreateIndex("dbo.Books", "ImageId");
            CreateIndex("dbo.Users", "ImageId");
            AddForeignKey("dbo.Authors", "ImageId", "dbo.Images", "ImageId");
            AddForeignKey("dbo.Books", "ImageId", "dbo.Images", "ImageId");
            AddForeignKey("dbo.Users", "ImageId", "dbo.Images", "ImageId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "ImageId", "dbo.Images");
            DropForeignKey("dbo.Books", "ImageId", "dbo.Images");
            DropForeignKey("dbo.Authors", "ImageId", "dbo.Images");
            DropIndex("dbo.Users", new[] { "ImageId" });
            DropIndex("dbo.Books", new[] { "ImageId" });
            DropIndex("dbo.Authors", new[] { "ImageId" });
            AlterColumn("dbo.Users", "ImageId", c => c.Int(nullable: false));
            AlterColumn("dbo.Books", "ImageId", c => c.Int(nullable: false));
            AlterColumn("dbo.Authors", "ImageId", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "Salt");
            CreateIndex("dbo.Users", "ImageId");
            CreateIndex("dbo.Books", "ImageId");
            CreateIndex("dbo.Authors", "ImageId");
            AddForeignKey("dbo.Users", "ImageId", "dbo.Images", "ImageId", cascadeDelete: true);
            AddForeignKey("dbo.Books", "ImageId", "dbo.Images", "ImageId", cascadeDelete: true);
            AddForeignKey("dbo.Authors", "ImageId", "dbo.Images", "ImageId", cascadeDelete: true);
        }
    }
}
