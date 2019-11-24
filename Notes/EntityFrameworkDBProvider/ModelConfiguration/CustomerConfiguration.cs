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
            Property(customer => customer.FirstName).HasColumnName("FirstName").HasMaxLength(50).IsRequired();
            Property(customer => customer.LastName).HasColumnName("LastName").HasMaxLength(50).IsRequired();
            Property(customer => customer.Login).HasColumnName("Login")
                .HasColumnType("nvarchar").HasMaxLength(26).IsRequired();
            HasIndex(customer => customer.Login).IsUnique(true);
            Property(customer => customer.Email).HasColumnName("Email").HasMaxLength(330).IsRequired();
            Property(customer => customer.Password).HasColumnName("Password").HasMaxLength(100).IsRequired();
            Property(customer => customer.LastLoginDate).HasColumnName("LastLoginDate").HasColumnType("datetime2").IsOptional();
        }
    }
}
