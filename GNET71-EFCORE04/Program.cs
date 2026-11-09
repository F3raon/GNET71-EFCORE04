using GNET71_EFCORE04.Data;
using GNET71_EFCORE04.Enums;
using GNET71_EFCORE04.Services;
using Microsoft.EntityFrameworkCore;

namespace GNET71_EFCORE04;

internal class Program
{
    private static void Main()
    {
        using var context = new ApplicationDbContext();
        try
        {
            context.Database.Migrate();
        }
        catch (Exception exception)
        {
            Console.WriteLine($"The database could not be initialized: {exception.Message}");
            return;
        }

        var customerService = new CustomerService(context);
        var accountService = new AccountService(context);
        var displayService = new DisplayService(context);

        while (true)
        {
            Console.Clear();
            Console.WriteLine("National Bank Management System");
            Console.WriteLine("1) Add a new Customer");
            Console.WriteLine("2) Open a new Account for a Customer");
            Console.WriteLine("3) Update Account Status (Active / Closed)");
            Console.WriteLine("4) Remove an Account from a Customer");
            Console.WriteLine("5) List all Customers (with accounts)");
            Console.WriteLine("0) Exit");
            Console.Write("Choose an option: ");

            var choice = Console.ReadLine();
            try
            {
                switch (choice)
                {
                    case "1":
                        AddCustomer(customerService);
                        Pause();
                        break;
                    case "2":
                        OpenAccount(accountService);
                        Pause();
                        break;
                    case "3":
                        UpdateStatus(accountService);
                        Pause();
                        break;
                    case "4":
                        RemoveOwnership(accountService);
                        Pause();
                        break;
                    case "5":
                        displayService.ListCustomersWithAccounts();
                        Pause();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Please choose a valid menu option.");
                        Pause();
                        break;
                }
            }
            catch (DbUpdateException)
            {
                Console.WriteLine("The operation could not be saved. Please check the entered values.");
                Pause();
            }
        }
    }

    private static void AddCustomer(CustomerService customerService)
    {
        Console.WriteLine("\nAdd a New Customer");
        var fullName = InputService.ReadRequiredString("Full Name: ");
        var nationalId = InputService.ReadRequiredString("National ID: ");
        var dateOfBirth = InputService.ReadDate("Date of Birth: ");
        var email = InputService.ReadRequiredString("Email: ");
        var phone = InputService.ReadOptionalString("Phone: ");
        var address = InputService.ReadOptionalString("Address: ");
        Console.WriteLine("1) Individual\n2) Business");
        var customerType = InputService.ReadEnumChoice<CustomerType>("Customer Type: ");

        var customer = customerService.AddCustomer(fullName, nationalId, dateOfBirth, email, phone, address, customerType);
        Console.WriteLine($"Customer created successfully. Customer Id: {customer.Id}");
    }

    private static void OpenAccount(AccountService accountService)
    {
        Console.WriteLine("\nOpen a New Account");
        var accountNumber = InputService.ReadRequiredString("Account Number: ");
        Console.WriteLine("1) Savings\n2) Current\n3) Business");
        var accountType = InputService.ReadEnumChoice<AccountType>("Account Type: ");
        var branchCode = InputService.ReadRequiredString("Branch Code: ");
        var customerId = InputService.ReadInt("Customer Id: ");
        Console.WriteLine("1) Primary\n2) CoHolder");
        var ownershipType = InputService.ReadEnumChoice<OwnershipType>("Ownership Role: ");

        var result = accountService.OpenAccount(accountNumber, accountType, branchCode, customerId, ownershipType);
        Console.WriteLine(result.Message);
    }

    private static void UpdateStatus(AccountService accountService)
    {
        Console.WriteLine("\nUpdate Account Status");
        var accountNumber = InputService.ReadRequiredString("Account Number: ");
        var customerId = InputService.ReadInt("Customer Id: ");
        Console.WriteLine("1) Active\n2) Closed");
        var status = InputService.ReadEnumChoice<AccountStatus>("New Status: ");

        var result = accountService.UpdateAccountStatus(accountNumber, customerId, status);
        Console.WriteLine(result.Message);
    }

    private static void RemoveOwnership(AccountService accountService)
    {
        Console.WriteLine("\nRemove an Account from a Customer");
        var accountNumber = InputService.ReadRequiredString("Account Number: ");
        var customerId = InputService.ReadInt("Customer Id: ");

        var result = accountService.RemoveAccountFromCustomer(accountNumber, customerId);
        Console.WriteLine(result.Message);
    }

    private static void Pause()
    {
        Console.WriteLine("\nPress any key to return to the menu...");
        Console.ReadKey();
    }
}
