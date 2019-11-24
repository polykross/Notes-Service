namespace Notes.EntityFrameworkDBProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotesMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 55),
                        LastName = c.String(nullable: false, maxLength: 55),
                        Login = c.String(nullable: false, maxLength: 26),
                        Email = c.String(nullable: false, maxLength: 330),
                        Password = c.String(nullable: false, maxLength: 100),
                        LastLoginDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Login, unique: true);
            
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 26),
                        Text = c.String(nullable: false, maxLength: 1000),
                        CreationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastEditDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        OwnerGuid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.OwnerGuid, cascadeDelete: true)
                .Index(t => t.OwnerGuid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notes", "OwnerGuid", "dbo.Customers");
            DropIndex("dbo.Notes", new[] { "OwnerGuid" });
            DropIndex("dbo.Customers", new[] { "Login" });
            DropTable("dbo.Notes");
            DropTable("dbo.Customers");
        }
    }
}
