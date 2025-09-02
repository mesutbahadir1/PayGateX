using PayGateX.Entities;

namespace PayGateX.Interfaces;

public interface ICurrencyRepository:IRepository<Currency>
{
    Task<bool> IsCurrencyExist(int id);
}