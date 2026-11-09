using GNET71_EFCORE04.Data;
using Microsoft.EntityFrameworkCore;

namespace GNET71_EFCORE04.Services;

public class DisplayService
{
    private readonly ApplicationDbContext _context;

    public DisplayService(ApplicationDbContext context)
    {
        _context = context;
    }

    public void ListCustomersWithAccounts()
    {
        var customers = _context.Customers
            .Include(customer => customer.CustomerAccounts)
            .ThenInclude(customerAccount => customerAccount.Account)
            .ThenInclude(account => account.Branch)
            .ToList();

        if (customers.Count == 0)
        {
            Console.WriteLine("No customers found.");
            return;
        }

        foreach (var customer in customers)
        {
            Console.WriteLine($"Customer Id: {customer.Id}");
            Console.WriteLine($"Name: {customer.FullName}");
            Console.WriteLine($"Type: {customer.CustomerType}");

            if (customer.CustomerAccounts.Count == 0)
            {
                Console.WriteLine("  (no accounts)");
            }
            else
            {
                foreach (var customerAccount in customer.CustomerAccounts)
                {
                    var account = customerAccount.Account;
                    Console.WriteLine($"  Account Number: {account.AccountNumber}");
                    Console.WriteLine($"    Balance: {account.CurrentBalance:N2}");
                    Console.WriteLine($"    Ownership: {customerAccount.OwnershipType}");
                    Console.WriteLine($"    Status: {customerAccount.AccountStatus}");
                    Console.WriteLine($"    Branch: {account.Branch.Name}");
                }
            }

            Console.WriteLine();
        }
    }
}
