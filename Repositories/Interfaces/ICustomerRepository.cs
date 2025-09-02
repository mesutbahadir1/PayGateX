using PayGateX.Entities;

namespace PayGateX.Interfaces;

public interface ICustomerRepository
{
    Task<List<Customer>> GetAllCustomers();
    
    Task<Customer> GetCustomerById(int id);
    
    Task<Customer> CreateCustomer(Customer customer);
    
    Task<Customer> UpdateCustomer(int id, Customer customer);
    
    Task<Customer> DeleteCustomer(int id);
    
    Task<bool> IsCustomerExist(int id);
    Task<List<Card>> GetCustomerCards(int customerId);
}