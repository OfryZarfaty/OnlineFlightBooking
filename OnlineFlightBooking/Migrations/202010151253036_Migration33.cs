namespace OnlineFlightBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration33 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Plains", "PlainNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Plains", "PlainNumber");
        }
    }
}
