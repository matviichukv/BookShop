namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCategoryPublisherUserReviewRoleentities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        ImageFile = c.Binary(nullable: false),
                    })
                .PrimaryKey(t => t.ImageId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        ThumbUp = c.Int(nullable: false),
                        ThumbDown = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 255),
                        UserName = c.String(nullable: false, maxLength: 255),
                        ImageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Images", t => t.ImageId, cascadeDelete: true)
                .Index(t => t.ImageId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.RoleUsers",
                c => new
                    {
                        Role_RoleId = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_RoleId, t.User_Id })
                .ForeignKey("dbo.Roles", t => t.Role_RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Role_RoleId)
                .Index(t => t.User_Id);
            
            AddColumn("dbo.Authors", "Description", c => c.String());
            AddColumn("dbo.Authors", "ImageId", c => c.Int(nullable: false));
            AddColumn("dbo.Books", "Description", c => c.String());
            AddColumn("dbo.Books", "Price", c => c.Int(nullable: false));
            AddColumn("dbo.Books", "Count", c => c.Int(nullable: false));
            AddColumn("dbo.Books", "ImageId", c => c.Int(nullable: false));
            AddColumn("dbo.Books", "Category_CategoryId", c => c.Int());
            CreateIndex("dbo.Authors", "ImageId");
            CreateIndex("dbo.Books", "ImageId");
            CreateIndex("dbo.Books", "Category_CategoryId");
            AddForeignKey("dbo.Authors", "ImageId", "dbo.Images", "ImageId", cascadeDelete: false);
            AddForeignKey("dbo.Books", "ImageId", "dbo.Images", "ImageId", cascadeDelete: false);
            AddForeignKey("dbo.Books", "Category_CategoryId", "dbo.Categories", "CategoryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "UserId", "dbo.Users");
            DropForeignKey("dbo.RoleUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.RoleUsers", "Role_RoleId", "dbo.Roles");
            DropForeignKey("dbo.Users", "ImageId", "dbo.Images");
            DropForeignKey("dbo.Reviews", "BookId", "dbo.Books");
            DropForeignKey("dbo.Books", "Category_CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Books", "ImageId", "dbo.Images");
            DropForeignKey("dbo.Authors", "ImageId", "dbo.Images");
            DropIndex("dbo.RoleUsers", new[] { "User_Id" });
            DropIndex("dbo.RoleUsers", new[] { "Role_RoleId" });
            DropIndex("dbo.Users", new[] { "ImageId" });
            DropIndex("dbo.Reviews", new[] { "BookId" });
            DropIndex("dbo.Reviews", new[] { "UserId" });
            DropIndex("dbo.Books", new[] { "Category_CategoryId" });
            DropIndex("dbo.Books", new[] { "ImageId" });
            DropIndex("dbo.Authors", new[] { "ImageId" });
            DropColumn("dbo.Books", "Category_CategoryId");
            DropColumn("dbo.Books", "ImageId");
            DropColumn("dbo.Books", "Count");
            DropColumn("dbo.Books", "Price");
            DropColumn("dbo.Books", "Description");
            DropColumn("dbo.Authors", "ImageId");
            DropColumn("dbo.Authors", "Description");
            DropTable("dbo.RoleUsers");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Reviews");
            DropTable("dbo.Categories");
            DropTable("dbo.Images");
        }
    }
}
