namespace Vidly.App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeColumnNumberInStocksToNumberInStock : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "NumberInStock", c => c.Byte(nullable: false));
            DropColumn("dbo.Movies", "NumberInStocks");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "NumberInStocks", c => c.Byte(nullable: false));
            DropColumn("dbo.Movies", "NumberInStock");
        }
    }
}
