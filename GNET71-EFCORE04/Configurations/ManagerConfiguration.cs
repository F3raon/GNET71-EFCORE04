using GNET71_EFCORE04.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GNET71_EFCORE04.Configurations;

public class ManagerConfiguration : IEntityTypeConfiguration<Manager>
{
    public void Configure(EntityTypeBuilder<Manager> builder)
    {
        builder.HasKey(manager => manager.Id);
        builder.Property(manager => manager.FullName).IsRequired().HasMaxLength(150);
        builder.Property(manager => manager.Email).IsRequired().HasMaxLength(150);
        builder.Property(manager => manager.PhoneNumber).HasMaxLength(30);
        builder.Property(manager => manager.BranchId).IsRequired();
        builder.HasIndex(manager => manager.BranchId).IsUnique();

        builder.HasData(
            new Manager { Id = 1, FullName = "Ahmed Hassan", Email = "ahmed.hassan@nationalbank.com", PhoneNumber = "01100000001", HireDate = new DateTime(2020, 1, 15), BranchId = 1 },
            new Manager { Id = 2, FullName = "Mona Ali", Email = "mona.ali@nationalbank.com", PhoneNumber = "01100000002", HireDate = new DateTime(2021, 6, 1), BranchId = 2 });
    }
}
