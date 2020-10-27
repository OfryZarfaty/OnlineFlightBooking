namespace OnlineFlightBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration331 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Flights", "Plain_PlainID", "dbo.Plains");
            DropIndex("dbo.Flights", new[] { "Plain_PlainID" });
            RenameColumn(table: "dbo.Flights", name: "Plain_PlainID", newName: "PlainID");
            AlterColumn("dbo.Flights", "PlainID", c => c.Int(nullable: false));
            CreateIndex("dbo.Flights", "PlainID");
            AddForeignKey("dbo.Flights", "PlainID", "dbo.Plains", "PlainID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Flights", "PlainID", "dbo.Plains");
            DropIndex("dbo.Flights", new[] { "PlainID" });
            AlterColumn("dbo.Flights", "PlainID", c => c.Int());
            RenameColumn(table: "dbo.Flights", name: "PlainID", newName: "Plain_PlainID");
            CreateIndex("dbo.Flights", "Plain_PlainID");
            AddForeignKey("dbo.Flights", "Plain_PlainID", "dbo.Plains", "PlainID");
        }
    }
}
