namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class follow : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Follower", "UserID", "dbo.User");
            DropIndex("dbo.Follower", new[] { "UserID" });
            CreateTable(
                "dbo.Following",
                c => new
                    {
                        FollowingID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        User_UserID = c.Int(),
                        Follow_UserID = c.Int(),
                        User_UserID1 = c.Int(),
                    })
                .PrimaryKey(t => t.FollowingID)
                .ForeignKey("dbo.User", t => t.User_UserID)
                .ForeignKey("dbo.User", t => t.Follow_UserID)
                .ForeignKey("dbo.User", t => t.User_UserID1)
                .Index(t => t.User_UserID)
                .Index(t => t.Follow_UserID)
                .Index(t => t.User_UserID1);
            
            DropTable("dbo.Follower");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Follower",
                c => new
                    {
                        FollowerID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FollowerID);
            
            DropForeignKey("dbo.Following", "User_UserID1", "dbo.User");
            DropForeignKey("dbo.Following", "Follow_UserID", "dbo.User");
            DropForeignKey("dbo.Following", "User_UserID", "dbo.User");
            DropIndex("dbo.Following", new[] { "User_UserID1" });
            DropIndex("dbo.Following", new[] { "Follow_UserID" });
            DropIndex("dbo.Following", new[] { "User_UserID" });
            DropTable("dbo.Following");
            CreateIndex("dbo.Follower", "UserID");
            AddForeignKey("dbo.Follower", "UserID", "dbo.User", "UserID", cascadeDelete: true);
        }
    }
}
