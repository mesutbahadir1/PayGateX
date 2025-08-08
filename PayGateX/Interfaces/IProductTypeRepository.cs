using PayGateX.Entities;

namespace PayGateX.Interfaces;

public interface IProductTypeRepository:IRepository<ProductType>
{
    Task<bool> IsProductTypeExist(int id);
}