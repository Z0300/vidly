namespace Vidly.App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsReturnToRentalHeaders : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RentalHeaders", "IsReturn", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RentalHeaders", "IsReturn");
        }
    }
}
