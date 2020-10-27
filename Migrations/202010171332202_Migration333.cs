namespace OnlineFlightBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration333 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Flights", "FlightStatus", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Flights", "FlightStatus", c => c.Int(nullable: false));
        }
    }
}
