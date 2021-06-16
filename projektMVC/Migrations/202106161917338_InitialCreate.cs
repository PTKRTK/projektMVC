namespace projektMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        AuthorID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                    })
                .PrimaryKey(t => t.AuthorID);
            
            CreateTable(
                "dbo.BookAuthors",
                c => new
                    {
                        BookAuthorID = c.Int(nullable: false, identity: true),
                        BookID = c.Int(nullable: false),
                        AuthorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookAuthorID)
                .ForeignKey("dbo.Authors", t => t.AuthorID, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.BookID, cascadeDelete: true)
                .Index(t => t.BookID)
                .Index(t => t.AuthorID);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookID = c.Int(nullable: false, identity: true),
                        BookTitle = c.String(),
                        PublicationYear = c.Int(nullable: false),
                        PublishingHouseID = c.Int(nullable: false),
                        BookCategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookID)
                .ForeignKey("dbo.BookCategories", t => t.BookCategoryID, cascadeDelete: true)
                .ForeignKey("dbo.PublishingHouses", t => t.PublishingHouseID, cascadeDelete: true)
                .Index(t => t.PublishingHouseID)
                .Index(t => t.BookCategoryID);
            
            CreateTable(
                "dbo.BookCategories",
                c => new
                    {
                        BookCategoryID = c.Int(nullable: false, identity: true),
                        CategoryTitle = c.String(),
                    })
                .PrimaryKey(t => t.BookCategoryID);
            
            CreateTable(
                "dbo.BookCopies",
                c => new
                    {
                        BookCopyID = c.Int(nullable: false, identity: true),
                        ISBN = c.Int(nullable: false),
                        BookCopyReleaseYear = c.Int(nullable: false),
                        BookID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookCopyID)
                .ForeignKey("dbo.Books", t => t.BookID, cascadeDelete: true)
                .Index(t => t.BookID);
            
            CreateTable(
                "dbo.Borrows",
                c => new
                    {
                        BorrowID = c.Int(nullable: false, identity: true),
                        BorrowDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(nullable: false),
                        BookCopyID = c.Int(nullable: false),
                        UserID = c.String(maxLength: 128),
                        PunishmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BorrowID)
                .ForeignKey("dbo.BookCopies", t => t.BookCopyID, cascadeDelete: true)
                .ForeignKey("dbo.Punishments", t => t.PunishmentID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.BookCopyID)
                .Index(t => t.UserID)
                .Index(t => t.PunishmentID);
            
            CreateTable(
                "dbo.Punishments",
                c => new
                    {
                        PunishmentID = c.Int(nullable: false, identity: true),
                        Charge = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.PunishmentID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
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
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.PublishingHouses",
                c => new
                    {
                        PublishingHouseID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        City = c.String(),
                    })
                .PrimaryKey(t => t.PublishingHouseID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Books", "PublishingHouseID", "dbo.PublishingHouses");
            DropForeignKey("dbo.Borrows", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Borrows", "PunishmentID", "dbo.Punishments");
            DropForeignKey("dbo.Borrows", "BookCopyID", "dbo.BookCopies");
            DropForeignKey("dbo.BookCopies", "BookID", "dbo.Books");
            DropForeignKey("dbo.Books", "BookCategoryID", "dbo.BookCategories");
            DropForeignKey("dbo.BookAuthors", "BookID", "dbo.Books");
            DropForeignKey("dbo.BookAuthors", "AuthorID", "dbo.Authors");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Borrows", new[] { "PunishmentID" });
            DropIndex("dbo.Borrows", new[] { "UserID" });
            DropIndex("dbo.Borrows", new[] { "BookCopyID" });
            DropIndex("dbo.BookCopies", new[] { "BookID" });
            DropIndex("dbo.Books", new[] { "BookCategoryID" });
            DropIndex("dbo.Books", new[] { "PublishingHouseID" });
            DropIndex("dbo.BookAuthors", new[] { "AuthorID" });
            DropIndex("dbo.BookAuthors", new[] { "BookID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PublishingHouses");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Punishments");
            DropTable("dbo.Borrows");
            DropTable("dbo.BookCopies");
            DropTable("dbo.BookCategories");
            DropTable("dbo.Books");
            DropTable("dbo.BookAuthors");
            DropTable("dbo.Authors");
        }
    }
}
