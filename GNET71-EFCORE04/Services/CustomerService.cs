using GNET71_EFCORE04.Data;
using GNET71_EFCORE04.Entities;
using GNET71_EFCORE04.Enums;

namespace GNET71_EFCORE04.Services;

public class CustomerService
{
    private readonly ApplicationDbContext _context;

    public CustomerService(ApplicationDbContext context)
    {
        _context = context;
    }

    public Customer AddCustomer(string fullName, string nationalId, DateTime dateOfBirth, string email, string? phoneNumber, string? address, CustomerType customerType)
    {
        var customer = new Customer
        {
            FullName = fullName,
            NationalId = nationalId,
            DateOfBirth = dateOfBirth,
            Email = email,
            PhoneNumber = phoneNumber,
            Address = address,
            CustomerType = customerType
        };

        _context.Customers.Add(customer);
        _context.SaveChanges();
        return customer;
    }
}
