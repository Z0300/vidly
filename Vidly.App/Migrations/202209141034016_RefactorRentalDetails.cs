namespace Vidly.App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactorRentalDetails : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.RentalDetails", "MovieId");
            AddForeignKey("dbo.RentalDetails", "MovieId", "dbo.Movies", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RentalDetails", "MovieId", "dbo.Movies");
            DropIndex("dbo.RentalDetails", new[] { "MovieId" });
        }
    }
}
