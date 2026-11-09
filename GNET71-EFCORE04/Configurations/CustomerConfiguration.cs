using GNET71_EFCORE04.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GNET71_EFCORE04.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(customer => customer.Id);
        builder.Property(customer => customer.NationalId).IsRequired().HasMaxLength(30);
        builder.HasIndex(customer => customer.NationalId).IsUnique();
        builder.Property(customer => customer.FullName).IsRequired().HasMaxLength(150);
        builder.Property(customer => customer.Email).IsRequired().HasMaxLength(150);
        builder.Property(customer => customer.PhoneNumber).HasMaxLength(30);
        builder.Property(customer => customer.Address).HasMaxLength(200);
        builder.Property(customer => customer.CustomerType).HasConversion<string>().HasMaxLength(20);
        builder.Property(customer => customer.DateOfBirth).IsRequired();
    }
}
