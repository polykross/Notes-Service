using System.Data.Entity.Migrations;

namespace Notes.EntityFrameworkDBProvider.Migrations
{
    class Configuration : DbMigrationsConfiguration<NotesDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}
