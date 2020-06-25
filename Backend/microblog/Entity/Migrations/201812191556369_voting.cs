namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class voting : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Voting",
                c => new
                    {
                        VotingID = c.Int(nullable: false, identity: true),
                        Status = c.Boolean(nullable: false),
                        UserID = c.Int(nullable: false),
                        TweetID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VotingID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Voting");
        }
    }
}
