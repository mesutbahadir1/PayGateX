using PayGateX.Entities;
using PayGateX.Interfaces;
using PayGateX.Service.Contracts;

namespace PayGateX.Service;

public class CurrencyService:ICurrencyService
{
    private readonly ICurrencyRepository _repository;
    public CurrencyService(ICurrencyRepository repository)
    {
        _repository = repository;
    }
    public async Task<List<Currency>> GetAll()
    {
        var currencies = await _repository.GetAll();;
        return currencies;
    }

    public async Task<Currency> GetById(int id)
    {
        var currency = await _repository.GetById(id);
        return currency;
    }

    public async Task<Currency> Create(Currency entity)
    {
        var createCurrency = await _repository.Create(entity);
        return createCurrency;
    }

    public async Task<Currency> Update(int id, Currency entity)
    {
        var currency = await _repository.Update(id, entity);
        return currency;
    }

    public async Task<Currency> Delete(int id)
    {
        var deletedCurrency = await _repository.Delete(id);
        return deletedCurrency;
    }
}