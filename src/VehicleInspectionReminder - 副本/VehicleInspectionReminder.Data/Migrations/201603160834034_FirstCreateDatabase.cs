namespace VehicleInspectionReminder.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstCreateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BrandName = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IllegalTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IllegalTypeName = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OwnerInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        LicenseNum = c.String(),
                        UserName = c.String(),
                        BirthDay = c.DateTime(),
                        Email = c.String(),
                        Gender = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VehicleInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BrandId = c.Int(nullable: false),
                        VehicleTypeId = c.Int(nullable: false),
                        Plate = c.String(),
                        DeliveryTtime = c.DateTime(nullable: false),
                        PurchaseDate = c.DateTime(nullable: false),
                        InsuranceStatus = c.Boolean(nullable: false),
                        FireCar = c.Int(nullable: false),
                        LightCondition = c.Int(nullable: false),
                        PlateIsIntact = c.Int(nullable: false),
                        LastInspectionTime = c.DateTime(),
                        OwnerId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.BrandId, cascadeDelete: true)
                .ForeignKey("dbo.VehicleTypes", t => t.VehicleTypeId, cascadeDelete: true)
                .ForeignKey("dbo.OwnerInfoes", t => t.OwnerId, cascadeDelete: true)
                .Index(t => t.BrandId)
                .Index(t => t.VehicleTypeId)
                .Index(t => t.OwnerId);
            
            CreateTable(
                "dbo.VehicleTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VehicleTypeName = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.T_User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 200),
                        RealName = c.String(nullable: false, maxLength: 200),
                        Email = c.String(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VehicleInfoes", "OwnerId", "dbo.OwnerInfoes");
            DropForeignKey("dbo.VehicleInfoes", "VehicleTypeId", "dbo.VehicleTypes");
            DropForeignKey("dbo.VehicleInfoes", "BrandId", "dbo.Brands");
            DropIndex("dbo.VehicleInfoes", new[] { "OwnerId" });
            DropIndex("dbo.VehicleInfoes", new[] { "VehicleTypeId" });
            DropIndex("dbo.VehicleInfoes", new[] { "BrandId" });
            DropTable("dbo.T_User");
            DropTable("dbo.VehicleTypes");
            DropTable("dbo.VehicleInfoes");
            DropTable("dbo.OwnerInfoes");
            DropTable("dbo.IllegalTypes");
            DropTable("dbo.Brands");
        }
    }
}
