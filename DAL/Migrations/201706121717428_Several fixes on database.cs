namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Severalfixesondatabase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "Category_CategoryId", "dbo.Categories");
            DropIndex("dbo.Books", new[] { "Category_CategoryId" });
            RenameColumn(table: "dbo.Books", name: "Category_CategoryId", newName: "CategoryId");
            AlterColumn("dbo.Books", "BookName", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "CategoryId", c => c.Int(nullable: false));
            AlterColumn("dbo.Categories", "CategoryName", c => c.String(nullable: false));
            AlterColumn("dbo.Roles", "RoleName", c => c.String(nullable: false));
            CreateIndex("dbo.Books", "CategoryId");
            AddForeignKey("dbo.Books", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Books", new[] { "CategoryId" });
            AlterColumn("dbo.Roles", "RoleName", c => c.String());
            AlterColumn("dbo.Categories", "CategoryName", c => c.String());
            AlterColumn("dbo.Books", "CategoryId", c => c.Int());
            AlterColumn("dbo.Books", "BookName", c => c.String());
            RenameColumn(table: "dbo.Books", name: "CategoryId", newName: "Category_CategoryId");
            CreateIndex("dbo.Books", "Category_CategoryId");
            AddForeignKey("dbo.Books", "Category_CategoryId", "dbo.Categories", "CategoryId");
        }
    }
}
