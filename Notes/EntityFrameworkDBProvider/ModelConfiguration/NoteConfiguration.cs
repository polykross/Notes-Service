using Notes.DBModels;
using System.Data.Entity.ModelConfiguration;

namespace Notes.EntityFrameworkDBProvider.ModelConfiguration
{
    class NoteConfiguration : EntityTypeConfiguration<Note>
    {
        public NoteConfiguration()
        {
            ToTable("Notes");
            HasKey(note => note.Guid);
            Property(note => note.Guid).HasColumnName("Id").IsRequired();
            Property(note => note.Title).HasColumnName("Title").HasMaxLength(26).IsRequired();
            Property(note => note.Text).HasColumnName("Text").HasMaxLength(1000).IsRequired();
            Property(note => note.CreationDate).HasColumnName("CreationDate").HasColumnType("datetime2").IsRequired();
            Property(note => note.LastEditDate).HasColumnName("LastEditDate").HasColumnType("datetime2").IsOptional();
        }
    }
}
