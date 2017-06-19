namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedOrdersDbSetandLikeDbSet : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        LikeId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ReviewId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LikeId)
                .ForeignKey("dbo.Reviews", t => t.ReviewId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.ReviewId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        OrderNumber = c.Int(nullable: false),
                        OrderPrice = c.Int(nullable: false),
                        BookCount = c.Int(nullable: false),
                        IsPaid = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.BookId);
            
            DropColumn("dbo.Books", "Test");
            DropColumn("dbo.Reviews", "ThumbUp");
            DropColumn("dbo.Reviews", "ThumbDown");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reviews", "ThumbDown", c => c.Int(nullable: false));
            AddColumn("dbo.Reviews", "ThumbUp", c => c.Int(nullable: false));
            AddColumn("dbo.Books", "Test", c => c.Int());
            DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
            DropForeignKey("dbo.Orders", "BookId", "dbo.Books");
            DropForeignKey("dbo.Likes", "UserId", "dbo.Users");
            DropForeignKey("dbo.Likes", "ReviewId", "dbo.Reviews");
            DropIndex("dbo.Orders", new[] { "BookId" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.Likes", new[] { "ReviewId" });
            DropIndex("dbo.Likes", new[] { "UserId" });
            DropTable("dbo.Orders");
            DropTable("dbo.Likes");
        }
    }
}
