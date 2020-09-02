namespace SpaParadise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        PhoneNo = c.String(nullable: false, maxLength: 10),
                        Password = c.String(nullable: false, maxLength: 100),
                        Address = c.String(),
                        Specialist = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AdminId);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CartId = c.Int(nullable: false, identity: true),
                        ServiceId = c.Int(nullable: false),
                        ServiceName = c.String(),
                        CustomerId = c.Int(nullable: false),
                        BookingDate = c.DateTime(),
                        BookingTime = c.DateTime(),
                        Specialist = c.String(),
                        Amount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.CartId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        PhoneNo = c.String(nullable: false, maxLength: 10),
                        Password = c.String(nullable: false, maxLength: 100),
                        Address = c.String(nullable: false, maxLength: 100),
                        WalletAmount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        ServiceId = c.Int(nullable: false, identity: true),
                        ServiceName = c.String(nullable: false),
                        ServiceType = c.String(nullable: false),
                        ServiceImage = c.String(),
                        Specialist = c.String(nullable: false),
                        ServiceDescription = c.String(nullable: false),
                        Amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ServiceId);
            
            AddColumn("dbo.Bookings", "ServiceId", c => c.Int(nullable: false));
            AddColumn("dbo.Bookings", "Specialist", c => c.String(nullable: false));
            AlterColumn("dbo.Bookings", "ServiceName", c => c.String(nullable: false));
            AlterColumn("dbo.Bookings", "BookingTime", c => c.DateTime());
            AlterColumn("dbo.Bookings", "BookingDate", c => c.DateTime());
            DropColumn("dbo.Bookings", "Specilist");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bookings", "Specilist", c => c.String());
            AlterColumn("dbo.Bookings", "BookingDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Bookings", "BookingTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Bookings", "ServiceName", c => c.String());
            DropColumn("dbo.Bookings", "Specialist");
            DropColumn("dbo.Bookings", "ServiceId");
            DropTable("dbo.Services");
            DropTable("dbo.Customers");
            DropTable("dbo.Carts");
            DropTable("dbo.Admins");
        }
    }
}
