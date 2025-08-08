using PayGateX.Entities;

namespace PayGateX.Interfaces;

public interface ICustomerTypeRepository:IRepository<CustomerType>
{
    Task<bool> IsCustomerTypeExist(int id);
}