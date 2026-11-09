using GNET71_EFCORE04.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GNET71_EFCORE04.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(account => account.AccountNumber);
        builder.Property(account => account.AccountNumber).HasMaxLength(30);
        builder.Property(account => account.CurrentBalance).HasPrecision(18, 2);
        builder.Property(account => account.AccountType).HasConversion<string>().HasMaxLength(20);
        builder.Property(account => account.OpeningDate).IsRequired();

        builder.HasOne(account => account.Branch)
            .WithMany(branch => branch.Accounts)
            .HasForeignKey(account => account.BranchId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
