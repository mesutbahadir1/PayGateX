using PayGateX.Entities;

namespace PayGateX.Interfaces;

public interface IPaymentMethodTypeRepository:IRepository<PaymentMethodType>
{
    Task<bool> IsPaymentMethodTypeExist(int id);
}