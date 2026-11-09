namespace GNET71_EFCORE04.Entities;

public class Manager
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public DateTime HireDate { get; set; }
    public int BranchId { get; set; }

    public Branch Branch { get; set; } = null!;
}
