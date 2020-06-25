namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userdetailsaddedtotweetstartend : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tweet", "Start", c => c.String());
            AddColumn("dbo.Tweet", "End", c => c.String());
            DropColumn("dbo.Tweet", "Fname");
            DropColumn("dbo.Tweet", "Lname");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tweet", "Lname", c => c.String());
            AddColumn("dbo.Tweet", "Fname", c => c.String());
            DropColumn("dbo.Tweet", "End");
            DropColumn("dbo.Tweet", "Start");
        }
    }
}
