using Notes.DBModels;
using System.Data.Entity.ModelConfiguration;

namespace Notes.EntityFrameworkDBProvider.ModelConfiguration
{
    class CustomerConfiguration : EntityTypeConfiguration<Customer>
    {
        public CustomerConfiguration()
        {
            ToTable("Customers");
            HasKey(customer => customer.Guid);
            Property(customer => customer.Guid).HasColumnName("Id").IsRequired();
            Property(customer => customer.Login).HasColumnName("Login")
                .HasColumnType("nvarchar")
                .HasMaxLength(26).IsRequired();
            Property(customer => customer.Password).HasColumnName("Password").IsRequired();
            HasIndex(customer => customer.Login).IsUnique(true);
        }
    }
}
