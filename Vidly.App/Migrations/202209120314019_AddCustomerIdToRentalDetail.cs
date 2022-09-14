namespace Vidly.App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerIdToRentalDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RentalDetails", "CustomerId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RentalDetails", "CustomerId");
        }
    }
}
