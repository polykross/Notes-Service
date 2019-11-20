using Notes.DBModels;
using Notes.EntityFrameworkDBProvider.ModelConfiguration;
using System.Data.Entity;
using Notes.EntityFrameworkDBProvider.Migrations;

namespace Notes.EntityFrameworkDBProvider
{
    public class NotesDBContext : DbContext
    {
        public NotesDBContext() : base("NotesDB")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NotesDBContext, Configuration>());
            Configuration.ProxyCreationEnabled = true;
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CustomerConfiguration());
            modelBuilder.Configurations.Add(new NoteConfiguration());
        }
    }
}
