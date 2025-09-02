using PayGateX.Entities;
using PayGateX.Interfaces;
using PayGateX.Service.Contracts;

namespace PayGateX.Service;

public class CustomerTypeService:ICustomerTypeService
{
    private readonly ICustomerTypeRepository _repository;
    public CustomerTypeService(ICustomerTypeRepository repository)
    {
        _repository = repository;
    }
    public async Task<List<CustomerType>> GetAll()
    {
        var customerTpyes = await _repository.GetAll();
        return customerTpyes;
    }

    public async Task<CustomerType> GetById(int id)
    {
        var customerTpye = await _repository.GetById(id);
        return customerTpye;
    }

    public async Task<CustomerType> Create(CustomerType entity)
    {
        var createCustomerType = await _repository.Create(entity);
        return createCustomerType;

    }

    public async Task<CustomerType> Update(int id, CustomerType entity)
    {
        var updateCustomerType = await _repository.Update(id, entity);
        return updateCustomerType;
    }

    public async Task<CustomerType> Delete(int id)
    {
        var deletedCustomerType = await _repository.Delete(id);
        return deletedCustomerType;
    }
}