using System.Data.Entity.Migrations;

namespace Notes.EntityFrameworkDBProvider.Migrations
{
    class Configuration : DbMigrationsConfiguration<NotesDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NotesDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
