namespace GNET71_EFCORE04.Entities;

public class Branch
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }

    public Manager Manager { get; set; } = null!;
    public ICollection<Account> Accounts { get; set; } = new HashSet<Account>();
}
