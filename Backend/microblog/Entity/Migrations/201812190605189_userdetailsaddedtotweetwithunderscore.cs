namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userdetailsaddedtotweetwithunderscore : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tweet", "First_Name", c => c.String());
            AddColumn("dbo.Tweet", "Last_Name", c => c.String());
            DropColumn("dbo.Tweet", "FirstName");
            DropColumn("dbo.Tweet", "LastName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tweet", "LastName", c => c.String());
            AddColumn("dbo.Tweet", "FirstName", c => c.String());
            DropColumn("dbo.Tweet", "Last_Name");
            DropColumn("dbo.Tweet", "First_Name");
        }
    }
}
