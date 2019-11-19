using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notes.DBModels;

namespace Notes.EntityFrameworkDBProvider.ModelConfiguration
{
    class CustomerConfiguration : EntityTypeConfiguration<Customer>
    {
        public CustomerConfiguration()
        {
            ToTable("Customer");
            HasKey(customer => customer.Guid);
            Property(customer => customer.Guid).HasColumnName("id").IsRequired();
            Property(customer => customer.Login).HasColumnName("login")
                .HasColumnType("nvarchar")
                .HasMaxLength(26).IsRequired();
            Property(customer => customer.Password).HasColumnName("password").IsRequired();
            HasIndex(customer => customer.Login).IsUnique(true);
        }
    }
}
