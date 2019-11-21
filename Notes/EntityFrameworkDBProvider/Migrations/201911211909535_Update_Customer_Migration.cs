namespace Notes.EntityFrameworkDBProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Customer_Migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Customers", "LastName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Customers", "Email", c => c.String(nullable: false, maxLength: 330));
            AddColumn("dbo.Customers", "LastLoginDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Customers", "Password", c => c.String(nullable: false, maxLength: 26));
            AlterColumn("dbo.Notes", "Title", c => c.String(nullable: false, maxLength: 26));
            AlterColumn("dbo.Notes", "Text", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("dbo.Notes", "LastEditDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Notes", "LastEditDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Notes", "Text", c => c.String(nullable: false));
            AlterColumn("dbo.Notes", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "Password", c => c.String(nullable: false));
            DropColumn("dbo.Customers", "LastLoginDate");
            DropColumn("dbo.Customers", "Email");
            DropColumn("dbo.Customers", "LastName");
            DropColumn("dbo.Customers", "FirstName");
        }
    }
}
