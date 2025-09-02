using PayGateX.Dtos.CustomerType;
using PayGateX.Entities;

namespace PayGateX.Mappers;

public static class CustomerTypeMappers
{
    public static CustomerTypeDto ToCustomerTypeDto(this CustomerType customer)
    {
        return new CustomerTypeDto()
        {
            Id = customer.Id,
            Code = customer.Code,
            Description = customer.Description
        };
    }
    public static CustomerType ToCustomerTypeFromCreateDto(this CreateCustomerTypeDto customerTypeDto)
    {
        return new CustomerType()
        {
           Code = customerTypeDto.Code,
           Description = customerTypeDto.Description
        };
    }
    
    public static CustomerType ToCustomerTypeFromUpdateDto(this UpdateCustomerTypeDto updateCustomerType)
    {
        return new CustomerType()
        {
            Description = updateCustomerType.Description,
            Code = updateCustomerType.Code
        };
    }
}