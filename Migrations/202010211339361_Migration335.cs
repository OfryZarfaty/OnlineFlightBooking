namespace OnlineFlightBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration335 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.People", "CreditCardID", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.People", "CreditCardID", c => c.Int(nullable: false));
        }
    }
}
