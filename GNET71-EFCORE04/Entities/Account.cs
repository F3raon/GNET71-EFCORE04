using GNET71_EFCORE04.Enums;

namespace GNET71_EFCORE04.Entities;

public class Account
{
    public string AccountNumber { get; set; } = string.Empty;
    public decimal CurrentBalance { get; set; }
    public AccountType AccountType { get; set; }
    public DateTime OpeningDate { get; set; }
    public int BranchId { get; set; }

    public Branch Branch { get; set; } = null!;
    public ICollection<CustomerAccount> CustomerAccounts { get; set; } = new HashSet<CustomerAccount>();
    public ICollection<Transaction> Transactions { get; set; } = new HashSet<Transaction>();
}
