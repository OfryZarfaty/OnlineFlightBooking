namespace OnlineFlightBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmptyMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.AdminID);
            
            CreateTable(
                "dbo.CreditCards",
                c => new
                    {
                        CreditCardID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        CardNumber = c.Int(nullable: false),
                        DateExpired = c.DateTime(nullable: false),
                        CVV = c.Int(nullable: false),
                        Person_PersonID = c.Int(),
                    })
                .PrimaryKey(t => t.CreditCardID)
                .ForeignKey("dbo.People", t => t.Person_PersonID)
                .Index(t => t.Person_PersonID);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        PersonID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        Permission = c.Int(nullable: false),
                        CreditCardID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PersonID);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ReservationID = c.Int(nullable: false, identity: true),
                        Flight_FlightID = c.Int(),
                        Person_PersonID = c.Int(),
                    })
                .PrimaryKey(t => t.ReservationID)
                .ForeignKey("dbo.Flights", t => t.Flight_FlightID)
                .ForeignKey("dbo.People", t => t.Person_PersonID)
                .Index(t => t.Flight_FlightID)
                .Index(t => t.Person_PersonID);
            
            CreateTable(
                "dbo.Flights",
                c => new
                    {
                        FlightID = c.Int(nullable: false, identity: true),
                        FlightNumber = c.String(),
                        FlightFromCountry = c.String(),
                        FlightToCountry = c.String(),
                        FlightPrice = c.Double(nullable: false),
                        FlightDateTimeTakeOff = c.DateTime(nullable: false),
                        FlightDuration = c.Int(nullable: false),
                        FlightDateTimeLanding = c.DateTime(nullable: false),
                        FlightTotalAviableSeats = c.Int(nullable: false),
                        FlightStatus = c.Int(nullable: false),
                        Plain_PlainID = c.Int(),
                    })
                .PrimaryKey(t => t.FlightID)
                .ForeignKey("dbo.Plains", t => t.Plain_PlainID)
                .Index(t => t.Plain_PlainID);
            
            CreateTable(
                "dbo.Plains",
                c => new
                    {
                        PlainID = c.Int(nullable: false, identity: true),
                        PlainTotalSeats = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlainID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CreditCards", "Person_PersonID", "dbo.People");
            DropForeignKey("dbo.Reservations", "Person_PersonID", "dbo.People");
            DropForeignKey("dbo.Flights", "Plain_PlainID", "dbo.Plains");
            DropForeignKey("dbo.Reservations", "Flight_FlightID", "dbo.Flights");
            DropIndex("dbo.Flights", new[] { "Plain_PlainID" });
            DropIndex("dbo.Reservations", new[] { "Person_PersonID" });
            DropIndex("dbo.Reservations", new[] { "Flight_FlightID" });
            DropIndex("dbo.CreditCards", new[] { "Person_PersonID" });
            DropTable("dbo.Plains");
            DropTable("dbo.Flights");
            DropTable("dbo.Reservations");
            DropTable("dbo.People");
            DropTable("dbo.CreditCards");
            DropTable("dbo.Admins");
        }
    }
}
