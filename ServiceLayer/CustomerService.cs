using Microsoft.AspNetCore.Identity;
using PayGateX.Dtos.Customer;
using PayGateX.Entities;
using PayGateX.Interfaces;
using PayGateX.Mappers;
using PayGateX.Service.Contracts;

namespace PayGateX.Service;

public class CustomerService:ICustomerService
{
    private readonly ICustomerRepository _repository;
    private readonly UserManager<AppUser> _userManager; 
    private readonly ICustomerTypeRepository _customerTypeRepository;
    public CustomerService(ICustomerRepository repository, UserManager<AppUser> userManager, ICustomerTypeRepository customerTypeRepository)
    {
        _repository = repository;
        _userManager = userManager;
        _customerTypeRepository = customerTypeRepository;
    }
    public async Task<List<Customer>> GetAllCustomers()
    {
        var customers = await _repository.GetAllCustomers();
        return customers;
    }

    public async Task<Customer> GetCustomerById(int id)
    {
        var customer = await _repository.GetCustomerById(id);
        return customer;
    }
    public async Task<List<Card>> GetCustomerCards(int customerId)
    {
        var customerCards = await _repository.GetCustomerCards(customerId);
        return customerCards;
    }

    public async Task<Customer> CreateCustomer(int customerTypeId,CreateCustomerDto createCustomerDto, string userName)
    {
        var appUser = await _userManager.FindByNameAsync(userName);

        var isCustomerTypeExist = await _customerTypeRepository.IsCustomerTypeExist(customerTypeId);
        if (!isCustomerTypeExist)
            return null;

        var customerModel = createCustomerDto.ToCustomerFromCreateDto(customerTypeId, appUser.Id);

        await _repository.CreateCustomer(customerModel);
        return customerModel;
    }

    public async Task<Customer> UpdateCustomer(int id, Customer customer)
    {
        var customerModel = await _repository.UpdateCustomer(id, customer);
        return customerModel;
    }

    public async Task<Customer> DeleteCustomer(int id)
    {
        var customerModel = await _repository.DeleteCustomer(id);
        return customerModel;
    }
    
}