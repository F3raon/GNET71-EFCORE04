using GNET71_EFCORE04.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GNET71_EFCORE04.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(transaction => transaction.TransactionNumber);
        builder.Property(transaction => transaction.TransactionNumber).HasMaxLength(30);
        builder.Property(transaction => transaction.Amount).HasPrecision(18, 2);
        builder.Property(transaction => transaction.TransactionType).HasConversion<string>().HasMaxLength(20);
        builder.Property(transaction => transaction.Note).HasMaxLength(300);

        builder.HasOne(transaction => transaction.Account)
            .WithMany(account => account.Transactions)
            .HasForeignKey(transaction => transaction.AccountNumber)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
