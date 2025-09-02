using Microsoft.EntityFrameworkCore;
using PayGateX.Data;
using PayGateX.Entities;
using PayGateX.Interfaces;

namespace PayGateX.Repository;

public class PaymentMethodTypeRepository:IPaymentMethodTypeRepository
{
    private readonly ApplicationDBContext _context;
    public PaymentMethodTypeRepository( ApplicationDBContext context)
    {
        _context = context;
    }
    public async Task<List<PaymentMethodType>> GetAll()
    {
       return await _context.PaymentMethodTypes.ToListAsync();
    }

    public async Task<PaymentMethodType> GetById(int id)
    {
        var paymentMethodType=await _context.PaymentMethodTypes.FirstOrDefaultAsync(x => x.Id == id);
        if (paymentMethodType==null)
            return null;
        return paymentMethodType;
    }

    public async Task<PaymentMethodType> Create(PaymentMethodType entity)
    {
        await _context.PaymentMethodTypes.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<PaymentMethodType> Update(int id, PaymentMethodType entity)
    {
        var existPaymentMethod = await _context.PaymentMethodTypes.FindAsync(id);
        if (existPaymentMethod==null)
            return null;
        
        existPaymentMethod.Code = entity.Code;
        existPaymentMethod.Description = entity.Description;
        await _context.SaveChangesAsync();
        return existPaymentMethod;
    }

    public async Task<PaymentMethodType> Delete(int id)
    {
        var existPaymentMethod = await _context.PaymentMethodTypes.FindAsync(id);
        if (existPaymentMethod==null)
            return null;
        
        _context.PaymentMethodTypes.Remove(existPaymentMethod);
        await _context.SaveChangesAsync();
        return existPaymentMethod;
    }

    public async Task<bool> IsPaymentMethodTypeExist(int id)
    {
        return await _context.PaymentMethodTypes.AnyAsync(x=>x.Id==id);
    }
}