using Microsoft.EntityFrameworkCore;
using PayGateX.Data;
using PayGateX.Entities;
using PayGateX.Interfaces;

namespace PayGateX.Repository;

public class CustomerTypeRepository:ICustomerTypeRepository
{
    private readonly ApplicationDBContext _context;
    public CustomerTypeRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    
    
    public async Task<List<CustomerType>> GetAll()
    {
        return await _context.CustomerTypes.ToListAsync();
    }

    public async Task<CustomerType> GetById(int id)
    {
        var customerType=await _context.CustomerTypes.FirstOrDefaultAsync(x => x.Id == id);
        if (customerType==null)
        {
            return null;
        }

        return customerType;
    }

    public async Task<CustomerType> Create(CustomerType customerType)
    {
        await _context.CustomerTypes.AddAsync(customerType);
        await _context.SaveChangesAsync();
        return customerType;
    }

    public async Task<CustomerType> Update(int id, CustomerType customerType)
    {
        var existingCustomerType = await _context.CustomerTypes.FirstOrDefaultAsync(x => x.Id == id);
        if (existingCustomerType==null)
        {
            return null;
        }
        existingCustomerType.Code = customerType.Code;
        existingCustomerType.Description = customerType.Description;
        await _context.SaveChangesAsync();
        return existingCustomerType;
    }

    public async Task<CustomerType> Delete(int id)
    {
        var customerType = await _context.CustomerTypes.FindAsync(id);
        if (customerType==null)
        {
            return null;
        }

        _context.CustomerTypes.Remove(customerType);
        await _context.SaveChangesAsync();
        return customerType;
    }
    
    public async Task<bool> IsCustomerTypeExist(int id)
    {
        return await _context.CustomerTypes.AnyAsync(x=>x.Id==id);

    }
}