using Microsoft.EntityFrameworkCore;
using PayGateX.Data;
using PayGateX.Entities;
using PayGateX.Interfaces;

namespace PayGateX.Repository;

public class ProductTypeRepository:IProductTypeRepository
{
    private readonly ApplicationDBContext _context;
    public ProductTypeRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    public async Task<List<ProductType>> GetAll()
    {
       return await _context.ProductTypes.ToListAsync();
    }

    public async Task<ProductType> GetById(int id)
    {
        var productType=await _context.ProductTypes.FirstOrDefaultAsync(x => x.Id == id);
        if (productType==null)
            return null;
        return productType;
    }

    public async Task<ProductType> Create(ProductType entity)
    {
        await _context.ProductTypes.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<ProductType> Update(int id, ProductType entity)
    {
        var existProductType = await _context.ProductTypes.FindAsync(id);
        if (existProductType==null)
            return null;
        
        existProductType.Code = entity.Code;
        existProductType.Description = entity.Description;
        await _context.SaveChangesAsync();
        return existProductType;
    }

    public async Task<ProductType> Delete(int id)
    {
        var existProductType = await _context.ProductTypes.FindAsync(id);
        if (existProductType==null)
            return null;

        _context.ProductTypes.Remove(existProductType);
        await _context.SaveChangesAsync();
        return existProductType;
    }

    public async Task<bool> IsProductTypeExist(int id)
    {
        return await _context.ProductTypes.AnyAsync(x=>x.Id==id);
    }
}