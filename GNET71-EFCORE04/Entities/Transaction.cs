using GNET71_EFCORE04.Enums;

namespace GNET71_EFCORE04.Entities;

public class Transaction
{
    public string TransactionNumber { get; set; } = string.Empty;
    public DateTime TransactionDate { get; set; }
    public decimal Amount { get; set; }
    public TransactionType TransactionType { get; set; }
    public string? Note { get; set; }
    public string AccountNumber { get; set; } = string.Empty;

    public Account Account { get; set; } = null!;
}
