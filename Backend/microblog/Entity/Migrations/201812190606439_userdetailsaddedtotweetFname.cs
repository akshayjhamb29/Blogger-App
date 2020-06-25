namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userdetailsaddedtotweetFname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tweet", "Fname", c => c.String());
            AddColumn("dbo.Tweet", "Lname", c => c.String());
            DropColumn("dbo.Tweet", "First_Name");
            DropColumn("dbo.Tweet", "Last_Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tweet", "Last_Name", c => c.String());
            AddColumn("dbo.Tweet", "First_Name", c => c.String());
            DropColumn("dbo.Tweet", "Lname");
            DropColumn("dbo.Tweet", "Fname");
        }
    }
}
