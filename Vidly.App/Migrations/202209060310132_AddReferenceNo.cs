namespace Vidly.App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReferenceNo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rentals", "RentalNo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rentals", "RentalNo");
        }
    }
}
