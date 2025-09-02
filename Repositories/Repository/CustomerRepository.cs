using Microsoft.EntityFrameworkCore;
using PayGateX.Data;
using PayGateX.Entities;
using PayGateX.Interfaces;

namespace PayGateX.Repository;

public class CustomerRepository:ICustomerRepository
{
    private readonly ApplicationDBContext _context;
    public CustomerRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    
    public async Task<List<Customer>> GetAllCustomers()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task<Customer> GetCustomerById(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer==null)
        {
            return null;
        }

        return customer;
    }

    public async Task<Customer> CreateCustomer(Customer customer)
    {
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task<Customer> UpdateCustomer(int id, Customer customer)
    {
        var customerModel = await _context.Customers.FindAsync(id);
        if (customerModel==null)
             return null;
        

        customerModel.Name = customer.Name;
        customerModel.Surname = customer.Surname;
        customerModel.Email = customer.Email;
        customerModel.CustomerNumber = customer.CustomerNumber;
        
        await _context.SaveChangesAsync();
        return customerModel;
    }

    public async Task<Customer> DeleteCustomer(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer==null)
        {
            return null;
        }

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task<bool> IsCustomerExist(int id)
    {
        return await _context.Customers.AnyAsync(x=>x.Id==id);
    }

    public async Task<List<Card>> GetCustomerCards(int customerId)
    {
        var customer = await _context.Customers.FindAsync(customerId);
        if (customer==null)
        {
            return null;
        }

        var allCustomerCards = await _context.Cards.Where(x => x.CustomerId == customerId).ToListAsync();
        return allCustomerCards;
    }
}