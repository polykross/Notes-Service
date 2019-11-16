using Notes.DBModels;
using System.Data.Entity.ModelConfiguration;

namespace Notes.EntityFrameworkDBProvider.ModelConfiguration
{
    class NoteConfiguration : EntityTypeConfiguration<Note>
    {
        public NoteConfiguration()
        {
            ToTable("Note");
            HasKey(note => note.Guid);
            Property(note => note.Guid).HasColumnName("id").IsRequired();
            Property(note => note.Title).HasColumnName("title").IsRequired();
            Property(note => note.Text).HasColumnName("text").IsRequired();
            Property(note => note.CreationDate).HasColumnName("creation_date").HasColumnType("datetime2").IsRequired();
            Property(note => note.LastEditDate).HasColumnName("last_edit_date").HasColumnType("datetime2").IsRequired();
        }
    }
}
