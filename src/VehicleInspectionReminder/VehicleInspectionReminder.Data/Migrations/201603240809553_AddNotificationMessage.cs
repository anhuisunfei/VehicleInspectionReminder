namespace VehicleInspectionReminder.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotificationMessage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NotificationMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        Body = c.String(),
                        IsSending = c.Boolean(nullable: false),
                        SendingTime = c.DateTime(),
                        VehicleInfoId = c.Int(nullable: false),
                        CheckTime = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VehicleInfoes", t => t.VehicleInfoId, cascadeDelete: true)
                .Index(t => t.VehicleInfoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NotificationMessages", "VehicleInfoId", "dbo.VehicleInfoes");
            DropIndex("dbo.NotificationMessages", new[] { "VehicleInfoId" });
            DropTable("dbo.NotificationMessages");
        }
    }
}
