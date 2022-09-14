namespace Vidly.App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveQtyToRentalDetails : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RentalDetails", "Qty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RentalDetails", "Qty", c => c.Int(nullable: false));
        }
    }
}
