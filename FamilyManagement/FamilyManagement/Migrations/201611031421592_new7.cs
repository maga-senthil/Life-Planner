namespace FamilyManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Adult = c.Boolean(nullable: false),
                        Child = c.Boolean(nullable: false),
                        Nanny = c.Boolean(nullable: false),
                        LoginId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.LoginId)
                .Index(t => t.LoginId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.People", "LoginId", "dbo.AspNetUsers");
            DropIndex("dbo.People", new[] { "LoginId" });
            DropTable("dbo.People");
        }
    }
}
