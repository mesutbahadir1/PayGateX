using PayGateX.Dtos.ProductType;
using PayGateX.Entities;

namespace PayGateX.Mappers;

public static class ProductTypeMappers
{
    public static ProductTypeDto ToProductTypeDto(this ProductType productType)
    {
        return new ProductTypeDto()
        {
            Id = productType.Id,
            Code = productType.Code,
            Description = productType.Description
        };
    }
    
    
    public static ProductType ToProductTypeFromCreateDto(this CreateProductTypeDto createProductTypeDto)
    {
        return new ProductType()
        {
            Code = createProductTypeDto.Code,
            Description = createProductTypeDto.Description
        };
    }
    
    public static ProductType ToProductTypeFromUpdateDto(this UpdateProductTypeDto updateProductTypeDto)
    {
        return new ProductType()
        {
            Description = updateProductTypeDto.Description,
            Code = updateProductTypeDto.Code
        };
    }
}