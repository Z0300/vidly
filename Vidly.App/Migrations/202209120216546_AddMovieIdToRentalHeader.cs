namespace Vidly.App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMovieIdToRentalHeader : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RentalDetails", "MovieId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RentalDetails", "MovieId");
        }
    }
}
