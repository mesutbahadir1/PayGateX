using PayGateX.Entities;
using PayGateX.Interfaces;
using PayGateX.Service.Contracts;

namespace PayGateX.Service;

public class ProductTypeService:IProductTypeService
{
    private readonly IProductTypeRepository _productTypeRepository;
    public ProductTypeService(IProductTypeRepository productTypeRepository)
    {
        _productTypeRepository = productTypeRepository;
    }
    public async Task<List<ProductType>> GetAll()
    {
        var productTypes = await _productTypeRepository.GetAll();;
        return productTypes;
    }

    public async Task<ProductType> GetById(int id)
    {
        var productType = await _productTypeRepository.GetById(id);
        return productType;
    }

    public async Task<ProductType> Create(ProductType entity)
    {
        var createProductType = await _productTypeRepository.Create(entity);
        return createProductType;
    }

    public async Task<ProductType> Update(int id, ProductType entity)
    {
        var productType = await _productTypeRepository.Update(id, entity);
        return productType;
    }

    public async Task<ProductType> Delete(int id)
    {
        var deletedProductType= await _productTypeRepository.Delete(id);
        return deletedProductType;
    }
}