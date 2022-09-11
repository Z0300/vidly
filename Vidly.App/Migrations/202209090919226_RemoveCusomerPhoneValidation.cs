namespace Vidly.App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCusomerPhoneValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "Phone", c => c.Long());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "Phone", c => c.Long(nullable: false));
        }
    }
}
