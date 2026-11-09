using GNET71_EFCORE04.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GNET71_EFCORE04.Configurations;

public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.HasKey(branch => branch.Id);
        builder.Property(branch => branch.Code).IsRequired().HasMaxLength(10);
        builder.HasIndex(branch => branch.Code).IsUnique();
        builder.Property(branch => branch.Name).IsRequired().HasMaxLength(100);
        builder.Property(branch => branch.Address).HasMaxLength(200);
        builder.Property(branch => branch.PhoneNumber).HasMaxLength(30);

        builder.HasOne(branch => branch.Manager)
            .WithOne(manager => manager.Branch)
            .HasForeignKey<Manager>(manager => manager.BranchId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new Branch { Id = 1, Code = "BR001", Name = "Downtown Branch", Address = "12 Nile Street", PhoneNumber = "01000000001" },
            new Branch { Id = 2, Code = "BR002", Name = "Garden City Branch", Address = "25 Garden Street", PhoneNumber = "01000000002" });
    }
}
