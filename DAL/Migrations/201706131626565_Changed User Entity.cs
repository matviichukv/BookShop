namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedUserEntity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RoleUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Reviews", "UserId", "dbo.Users");
            RenameColumn(table: "dbo.RoleUsers", name: "User_Id", newName: "User_UserId");
            RenameIndex(table: "dbo.RoleUsers", name: "IX_User_Id", newName: "IX_User_UserId");
            DropPrimaryKey("dbo.Users");
            DropColumn("dbo.Users", "Id");
            DropColumn("dbo.Users", "Login");
            DropColumn("dbo.Users", "Email");
            DropColumn("dbo.Users", "Password");
            AddColumn("dbo.Users", "UserId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Users", "UserEmail", c => c.String(nullable: false));
            AddColumn("dbo.Users", "UserPassword", c => c.String(nullable: false, maxLength: 255));
            AddPrimaryKey("dbo.Users", "UserId");
            AddForeignKey("dbo.RoleUsers", "User_UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.Reviews", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.Users", "Email", c => c.String(nullable: false));
            AddColumn("dbo.Users", "Login", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.Users", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Reviews", "UserId", "dbo.Users");
            DropForeignKey("dbo.RoleUsers", "User_UserId", "dbo.Users");
            DropPrimaryKey("dbo.Users");
            DropColumn("dbo.Users", "UserPassword");
            DropColumn("dbo.Users", "UserEmail");
            DropColumn("dbo.Users", "UserId");
            AddPrimaryKey("dbo.Users", "Id");
            RenameIndex(table: "dbo.RoleUsers", name: "IX_User_UserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.RoleUsers", name: "User_UserId", newName: "User_Id");
            AddForeignKey("dbo.Reviews", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RoleUsers", "User_Id", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
