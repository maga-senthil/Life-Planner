namespace FamilyManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmergencyContacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        StreetAddress = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.Int(nullable: false),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Maintanances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Item = c.String(),
                        LastMaintenance = c.DateTime(nullable: false),
                        MaintenanceFrequency = c.Int(nullable: false),
                        NextMaintenance = c.DateTime(nullable: false),
                        MaintenanceCost = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Maintanances");
            DropTable("dbo.EmergencyContacts");
        }
    }
}
