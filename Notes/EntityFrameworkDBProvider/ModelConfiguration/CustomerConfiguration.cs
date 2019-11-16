using Notes.DBModels;
using System.Data.Entity.ModelConfiguration;

namespace Notes.EntityFrameworkDBProvider.ModelConfiguration
{
    class CustomerConfiguration : EntityTypeConfiguration<Customer>
    {
        public CustomerConfiguration()
        {
            ToTable("Customer");
            HasKey(customer => customer.Guid);
            Property(customer => customer.Guid).HasColumnName("id").IsRequired();
            Property(customer => customer.Login).HasColumnName("login").IsRequired();
            Property(customer => customer.Password).HasColumnName("password").IsRequired();
            HasIndex(customer => customer.Login).IsUnique(true);
        }
    }
}
