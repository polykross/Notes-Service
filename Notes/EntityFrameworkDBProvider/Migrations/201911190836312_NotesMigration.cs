namespace Notes.EntityFrameworkDBProvider.Migrations
{
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
                        Login = c.String(nullable: false, maxLength: 26),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Login, unique: true);
            
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        Text = c.String(nullable: false),
                        CreationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastEditDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
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
