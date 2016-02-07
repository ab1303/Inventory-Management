namespace InventoryManagement.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Models : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cartons",
                c => new
                    {
                        CartonId = c.Int(nullable: false, identity: true),
                        NumberOfPieces = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CartonId)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemId = c.Int(nullable: false, identity: true),
                        ItemName = c.String(),
                        ItemCode = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ItemId);
            
            CreateTable(
                "dbo.Issues",
                c => new
                    {
                        IssueID = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        Body = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        IssueType = c.Int(nullable: false),
                        AssignedTo_Id = c.String(nullable: false, maxLength: 128),
                        Creator_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IssueID)
                .ForeignKey("dbo.AspNetUsers", t => t.AssignedTo_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Creator_Id)
                .Index(t => t.AssignedTo_Id)
                .Index(t => t.Creator_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.LoginProvider, t.ProviderKey })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LogActions",
                c => new
                    {
                        LogActionID = c.Int(nullable: false, identity: true),
                        PerformedAt = c.DateTime(nullable: false),
                        Controller = c.String(),
                        Action = c.String(),
                        Description = c.String(),
                        PerformedBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.LogActionID)
                .ForeignKey("dbo.AspNetUsers", t => t.PerformedBy_Id)
                .Index(t => t.PerformedBy_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LogActions", "PerformedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Issues", "Creator_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Issues", "AssignedTo_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Cartons", "ItemId", "dbo.Items");
            DropIndex("dbo.LogActions", new[] { "PerformedBy_Id" });
            DropIndex("dbo.Issues", new[] { "Creator_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "User_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Issues", new[] { "AssignedTo_Id" });
            DropIndex("dbo.Cartons", new[] { "ItemId" });
            DropTable("dbo.LogActions");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Issues");
            DropTable("dbo.Items");
            DropTable("dbo.Cartons");
        }
    }
}
