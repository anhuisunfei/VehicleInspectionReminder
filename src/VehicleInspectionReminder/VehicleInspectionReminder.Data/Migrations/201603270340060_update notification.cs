namespace VehicleInspectionReminder.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatenotification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NotificationMessages", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.NotificationMessages", "Email");
        }
    }
}
