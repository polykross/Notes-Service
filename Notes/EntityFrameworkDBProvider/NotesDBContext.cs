﻿using Notes.DBModels;
using Notes.EntityFrameworkDBProvider.Migrations;
using Notes.EntityFrameworkDBProvider.ModelConfiguration;
using System.Data.Entity;

namespace Notes.EntityFrameworkDBProvider
{
    public class NotesDBContext : DbContext
    {
        public NotesDBContext() : base("NotesDB")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NotesDBContext, Configuration>());
            Configuration.ProxyCreationEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CustomerConfiguration());
            modelBuilder.Configurations.Add(new NoteConfiguration());
        }
    }
}
