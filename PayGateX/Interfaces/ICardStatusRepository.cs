using PayGateX.Entities;

namespace PayGateX.Interfaces;

public interface ICardStatusRepository:IRepository<CardStatus>
{
    Task<bool> IsCardStatusExist(int id);
}