namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userdetailsaddedtotweet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tweet", "FirstName", c => c.String());
            AddColumn("dbo.Tweet", "LastName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tweet", "LastName");
            DropColumn("dbo.Tweet", "FirstName");
        }
    }
}
