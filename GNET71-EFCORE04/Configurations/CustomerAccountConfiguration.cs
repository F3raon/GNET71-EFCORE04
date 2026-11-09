using GNET71_EFCORE04.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GNET71_EFCORE04.Configurations;

public class CustomerAccountConfiguration : IEntityTypeConfiguration<CustomerAccount>
{
    public void Configure(EntityTypeBuilder<CustomerAccount> builder)
    {
        builder.HasKey(customerAccount => new { customerAccount.CustomerId, customerAccount.AccountNumber });
        builder.Property(customerAccount => customerAccount.AccountNumber).HasMaxLength(30);
        builder.Property(customerAccount => customerAccount.OwnershipType).HasConversion<string>().HasMaxLength(20);
        builder.Property(customerAccount => customerAccount.AccountStatus).HasConversion<string>().HasMaxLength(20);
        builder.Property(customerAccount => customerAccount.OwnershipStartDate).IsRequired();

        builder.HasOne(customerAccount => customerAccount.Customer)
            .WithMany(customer => customer.CustomerAccounts)
            .HasForeignKey(customerAccount => customerAccount.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(customerAccount => customerAccount.Account)
            .WithMany(account => account.CustomerAccounts)
            .HasForeignKey(customerAccount => customerAccount.AccountNumber)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
