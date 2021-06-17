namespace projektMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Annoucments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Announcements",
                c => new
                    {
                        AnnouncementID = c.Int(nullable: false, identity: true),
                        PublicationDate = c.DateTime(nullable: false),
                        AnnouncementText = c.String(),
                    })
                .PrimaryKey(t => t.AnnouncementID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Announcements");
        }
    }
}
