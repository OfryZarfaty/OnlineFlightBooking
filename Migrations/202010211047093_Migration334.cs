namespace OnlineFlightBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration334 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservations", "Person_PersonID", "dbo.People");
            DropForeignKey("dbo.Reservations", "Flight_FlightID", "dbo.Flights");
            DropIndex("dbo.Reservations", new[] { "Flight_FlightID" });
            DropIndex("dbo.Reservations", new[] { "Person_PersonID" });
            RenameColumn(table: "dbo.Reservations", name: "Person_PersonID", newName: "PersonID");
            RenameColumn(table: "dbo.Reservations", name: "Flight_FlightID", newName: "FlightID");
            AddColumn("dbo.Reservations", "FinalPrice", c => c.Double(nullable: false));
            AlterColumn("dbo.Reservations", "FlightID", c => c.Int(nullable: true));
            AlterColumn("dbo.Reservations", "PersonID", c => c.Int(nullable: true));
            CreateIndex("dbo.Reservations", "PersonID");
            CreateIndex("dbo.Reservations", "FlightID");
            AddForeignKey("dbo.Reservations", "PersonID", "dbo.People", "PersonID", cascadeDelete: true);
            AddForeignKey("dbo.Reservations", "FlightID", "dbo.Flights", "FlightID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "FlightID", "dbo.Flights");
            DropForeignKey("dbo.Reservations", "PersonID", "dbo.People");
            DropIndex("dbo.Reservations", new[] { "FlightID" });
            DropIndex("dbo.Reservations", new[] { "PersonID" });
            AlterColumn("dbo.Reservations", "PersonID", c => c.Int());
            AlterColumn("dbo.Reservations", "FlightID", c => c.Int());
            DropColumn("dbo.Reservations", "FinalPrice");
            RenameColumn(table: "dbo.Reservations", name: "FlightID", newName: "Flight_FlightID");
            RenameColumn(table: "dbo.Reservations", name: "PersonID", newName: "Person_PersonID");
            CreateIndex("dbo.Reservations", "Person_PersonID");
            CreateIndex("dbo.Reservations", "Flight_FlightID");
            AddForeignKey("dbo.Reservations", "Flight_FlightID", "dbo.Flights", "FlightID");
            AddForeignKey("dbo.Reservations", "Person_PersonID", "dbo.People", "PersonID");
        }
    }
}
