namespace Vidly.App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactorRentalHeaderDetails : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RentalHeaders", "MovieId", "dbo.Movies");
            DropIndex("dbo.RentalHeaders", new[] { "MovieId" });
            AddColumn("dbo.RentalDetails", "RentalHeaderId", c => c.Int(nullable: false));
            AddColumn("dbo.RentalDetails", "DateRented", c => c.DateTime(nullable: false));
            AddColumn("dbo.RentalDetails", "DateReturned", c => c.DateTime());
            AddColumn("dbo.RentalDetails", "IsReturn", c => c.Boolean(nullable: false));
            CreateIndex("dbo.RentalDetails", "RentalHeaderId");
            AddForeignKey("dbo.RentalDetails", "RentalHeaderId", "dbo.RentalHeaders", "Id", cascadeDelete: true);
            DropColumn("dbo.RentalDetails", "RentalNo");
            DropColumn("dbo.RentalDetails", "CustomerId");
            DropColumn("dbo.RentalHeaders", "MovieId");
            DropColumn("dbo.RentalHeaders", "DateRented");
            DropColumn("dbo.RentalHeaders", "IsReturn");
            DropColumn("dbo.RentalHeaders", "DateReturned");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RentalHeaders", "DateReturned", c => c.DateTime());
            AddColumn("dbo.RentalHeaders", "IsReturn", c => c.Boolean(nullable: false));
            AddColumn("dbo.RentalHeaders", "DateRented", c => c.DateTime(nullable: false));
            AddColumn("dbo.RentalHeaders", "MovieId", c => c.Int(nullable: false));
            AddColumn("dbo.RentalDetails", "CustomerId", c => c.Int(nullable: false));
            AddColumn("dbo.RentalDetails", "RentalNo", c => c.String());
            DropForeignKey("dbo.RentalDetails", "RentalHeaderId", "dbo.RentalHeaders");
            DropIndex("dbo.RentalDetails", new[] { "RentalHeaderId" });
            DropColumn("dbo.RentalDetails", "IsReturn");
            DropColumn("dbo.RentalDetails", "DateReturned");
            DropColumn("dbo.RentalDetails", "DateRented");
            DropColumn("dbo.RentalDetails", "RentalHeaderId");
            CreateIndex("dbo.RentalHeaders", "MovieId");
            AddForeignKey("dbo.RentalHeaders", "MovieId", "dbo.Movies", "Id", cascadeDelete: true);
        }
    }
}
