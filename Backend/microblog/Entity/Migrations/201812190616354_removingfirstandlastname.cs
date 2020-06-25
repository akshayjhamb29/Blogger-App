namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removingfirstandlastname : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tweet", "Start");
            DropColumn("dbo.Tweet", "End");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tweet", "End", c => c.String());
            AddColumn("dbo.Tweet", "Start", c => c.String());
        }
    }
}
