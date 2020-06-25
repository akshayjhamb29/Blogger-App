namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class database : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Follower",
                c => new
                    {
                        FollowerID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FollowerID)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        EmailID = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        ProfileImage = c.String(),
                        Contact = c.String(),
                        Country = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.Tweet",
                c => new
                    {
                        TweetID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TweetID)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tweet", "UserID", "dbo.User");
            DropForeignKey("dbo.Follower", "UserID", "dbo.User");
            DropIndex("dbo.Tweet", new[] { "UserID" });
            DropIndex("dbo.Follower", new[] { "UserID" });
            DropTable("dbo.Tweet");
            DropTable("dbo.User");
            DropTable("dbo.Follower");
        }
    }
}
