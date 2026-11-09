using Microsoft.EntityFrameworkCore;

namespace GNET71_EFCORE04.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Entities.Branch> Branches => Set<Entities.Branch>();
    public DbSet<Entities.Manager> Managers => Set<Entities.Manager>();
    public DbSet<Entities.Account> Accounts => Set<Entities.Account>();
    public DbSet<Entities.Customer> Customers => Set<Entities.Customer>();
    public DbSet<Entities.CustomerAccount> CustomerAccounts => Set<Entities.CustomerAccount>();
    public DbSet<Entities.Transaction> Transactions => Set<Entities.Transaction>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database= GNET71-EFCORE04;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
