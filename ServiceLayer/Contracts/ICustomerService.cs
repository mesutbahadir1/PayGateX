using PayGateX.Dtos.Customer;
using PayGateX.Entities;

namespace PayGateX.Service.Contracts;

public interface ICustomerService
{
    Task<List<Customer>> GetAllCustomers();
    
    Task<Customer> GetCustomerById(int id);
    
    Task<Customer> CreateCustomer(int customerTypeId,CreateCustomerDto createCustomerDto, string userName);
    
    Task<Customer> UpdateCustomer(int id, Customer customer);
    
    Task<Customer> DeleteCustomer(int id);
    Task<List<Card>> GetCustomerCards(int customerId);
}