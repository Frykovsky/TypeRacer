namespace TypeRacerDomain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UsersRaces",
                c => new
                    {
                        RaceGlobalID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Speed = c.Int(nullable: false),
                        Mistakes = c.Int(nullable: false),
                        RaceDate = c.DateTime(nullable: false),
                        TrackID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RaceGlobalID)
                .ForeignKey("dbo.Tracks", t => t.TrackID, cascadeDelete: true)
                .Index(t => t.TrackID);
            
            CreateTable(
                "dbo.Tracks",
                c => new
                    {
                        TrackID = c.Int(nullable: false, identity: true),
                        UploadDate = c.DateTime(nullable: false),
                        Uploader = c.String(),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.TrackID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsersRaces", "TrackID", "dbo.Tracks");
            DropIndex("dbo.UsersRaces", new[] { "TrackID" });
            DropTable("dbo.Tracks");
            DropTable("dbo.UsersRaces");
        }
    }
}
