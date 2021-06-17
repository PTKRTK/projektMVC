namespace projektMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isBorrowed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookCopies", "IsBorrowed", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BookCopies", "IsBorrowed");
        }
    }
}
