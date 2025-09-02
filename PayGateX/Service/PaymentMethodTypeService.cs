using PayGateX.Entities;
using PayGateX.Interfaces;
using PayGateX.Service.Contracts;

namespace PayGateX.Service;

public class PaymentMethodTypeService:IPaymentMethodTypeService
{
    private readonly IPaymentMethodTypeRepository _repository;
    public PaymentMethodTypeService(IPaymentMethodTypeRepository repository)
    {
        _repository = repository;
    }
    public async Task<List<PaymentMethodType>> GetAll()
    {
        var paymentMethodTypes = await _repository.GetAll();
        return paymentMethodTypes;
    }

    public async Task<PaymentMethodType> GetById(int id)
    {
        var paymentMethodType = await _repository.GetById(id);
        return paymentMethodType;
    }

    public async Task<PaymentMethodType> Create(PaymentMethodType entity)
    {
        var createPaymentMethodType = await _repository.Create(entity);
        return createPaymentMethodType;
    }

    public async Task<PaymentMethodType> Update(int id, PaymentMethodType entity)
    {
        var paymentMethod = await _repository.Update(id,entity);
        return paymentMethod;
    }

    public async Task<PaymentMethodType> Delete(int id)
    {
        var paymentMethod = await _repository.Delete(id);
        return paymentMethod;
    }
}