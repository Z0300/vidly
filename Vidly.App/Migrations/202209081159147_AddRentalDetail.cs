namespace Vidly.App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRentalDetail : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Rentals", "RentalDetailId", "dbo.RentalDetails");
            DropIndex("dbo.Rentals", new[] { "RentalDetailId" });
            DropColumn("dbo.Rentals", "RentalDetailId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rentals", "RentalDetailId", c => c.Int(nullable: false));
            CreateIndex("dbo.Rentals", "RentalDetailId");
            AddForeignKey("dbo.Rentals", "RentalDetailId", "dbo.RentalDetails", "Id", cascadeDelete: true);
        }
    }
}
