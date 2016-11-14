namespace FamilyManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Chores = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DailyChores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FamilyId = c.Int(nullable: false),
                        ChoreId = c.Int(nullable: false),
                        ChoreDay = c.DateTime(nullable: false),
                        Done = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Chores", t => t.ChoreId, cascadeDelete: true)
                .ForeignKey("dbo.Families", t => t.FamilyId, cascadeDelete: true)
                .Index(t => t.FamilyId)
                .Index(t => t.ChoreId);
            
            CreateTable(
                "dbo.Families",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Points = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        title = c.String(),
                        start = c.String(),
                        end = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DailyChores", "FamilyId", "dbo.Families");
            DropForeignKey("dbo.DailyChores", "ChoreId", "dbo.Chores");
            DropIndex("dbo.DailyChores", new[] { "ChoreId" });
            DropIndex("dbo.DailyChores", new[] { "FamilyId" });
            DropTable("dbo.Events");
            DropTable("dbo.Families");
            DropTable("dbo.DailyChores");
            DropTable("dbo.Chores");
        }
    }
}
