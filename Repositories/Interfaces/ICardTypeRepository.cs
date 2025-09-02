using PayGateX.Entities;

namespace PayGateX.Interfaces;

public interface ICardTypeRepository:IRepository<CardType>
{
    Task<bool> IsCardTypeExist(int id);
}