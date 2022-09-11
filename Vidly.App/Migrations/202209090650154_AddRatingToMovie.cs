namespace Vidly.App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRatingToMovie : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "Rating", c => c.Int(nullable: false));
            DropColumn("dbo.Movies", "Cost");
            DropColumn("dbo.RentalDetails", "TotalCost");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RentalDetails", "TotalCost", c => c.Double(nullable: false));
            AddColumn("dbo.Movies", "Cost", c => c.Double());
            DropColumn("dbo.Movies", "Rating");
        }
    }
}
