using GNET71_EFCORE04.Data;
using GNET71_EFCORE04.Entities;
using GNET71_EFCORE04.Enums;
using Microsoft.EntityFrameworkCore;

namespace GNET71_EFCORE04.Services;

public class AccountService
{
    private readonly ApplicationDbContext _context;

    public AccountService(ApplicationDbContext context)
    {
        _context = context;
    }

    public (bool Success, string Message) OpenAccount(string accountNumber, AccountType accountType, string branchCode, int customerId, OwnershipType ownershipType)
    {
        var branch = _context.Branches.SingleOrDefault(branch => branch.Code == branchCode);
        if (branch is null)
        {
            return (false, "The specified branch was not found.");
        }

        var customerExists = _context.Customers.Any(customer => customer.Id == customerId);
        if (!customerExists)
        {
            return (false, "The specified customer was not found.");
        }

        if (_context.Accounts.Any(account => account.AccountNumber == accountNumber))
        {
            return (false, "An account with this account number already exists.");
        }

        using var transaction = _context.Database.BeginTransaction();
        try
        {
            var account = new Account
            {
                AccountNumber = accountNumber,
                AccountType = accountType,
                BranchId = branch.Id,
                CurrentBalance = 0,
                OpeningDate = DateTime.Now
            };

            _context.Accounts.Add(account);
            _context.CustomerAccounts.Add(new CustomerAccount
            {
                CustomerId = customerId,
                AccountNumber = accountNumber,
                OwnershipType = ownershipType,
                OwnershipStartDate = DateTime.Now,
                AccountStatus = AccountStatus.Active
            });

            _context.SaveChanges();
            transaction.Commit();
            return (true, $"Account '{accountNumber}' was opened and linked to customer {customerId}.");
        }
        catch (DbUpdateException)
        {
            transaction.Rollback();
            return (false, "The account could not be saved. Please check the entered values.");
        }
    }

    public (bool Success, string Message) UpdateAccountStatus(string accountNumber, int customerId, AccountStatus accountStatus)
    {
        var customerAccount = _context.CustomerAccounts.Find(customerId, accountNumber);
        if (customerAccount is null)
        {
            return (false, "The customer-account relationship was not found.");
        }

        customerAccount.AccountStatus = accountStatus;
        _context.SaveChanges();
        return (true, $"Account status updated to {accountStatus}.");
    }

    public (bool Success, string Message) RemoveAccountFromCustomer(string accountNumber, int customerId)
    {
        var customerAccount = _context.CustomerAccounts.Find(customerId, accountNumber);
        if (customerAccount is null)
        {
            return (false, "The customer-account relationship was not found.");
        }

        using var transaction = _context.Database.BeginTransaction();
        try
        {
            _context.CustomerAccounts.Remove(customerAccount);
            _context.SaveChanges();

            var hasOtherOwners = _context.CustomerAccounts.Any(link => link.AccountNumber == accountNumber);
            var accountDeleted = false;
            if (!hasOtherOwners)
            {
                var account = _context.Accounts.Find(accountNumber);
                if (account is not null)
                {
                    _context.Accounts.Remove(account);
                    _context.SaveChanges();
                    accountDeleted = true;
                }
            }

            transaction.Commit();
            var message = accountDeleted
                ? $"Ownership link deleted. Account '{accountNumber}' was also removed because it had no other owners."
                : $"Ownership link for account '{accountNumber}' was deleted. The account still has another owner.";
            return (true, message);
        }
        catch (DbUpdateException)
        {
            transaction.Rollback();
            return (false, "The ownership link could not be removed.");
        }
    }
}
