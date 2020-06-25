namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addressaddedtotweet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tweet", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tweet", "Address");
        }
    }
}
