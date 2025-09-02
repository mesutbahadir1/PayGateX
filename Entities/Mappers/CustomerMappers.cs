using PayGateX.Dtos.Customer;
using PayGateX.Entities;

namespace PayGateX.Mappers;

public static class CustomerMappers
{
    public static CustomerDto ToCustomerDto(this Customer customer)
    {
        return new CustomerDto()
        {
            Id = customer.Id,
            Name=customer.Name,
            Surname=customer.Surname,
            Email=customer.Email,
            CreatedAt=customer.CreatedAt,
            CustomerNumber=customer.CustomerNumber,
            CustomerTypeId=customer.CustomerTypeId,
            AppUserId=customer.AppUserId
        };
    }
    
    
    public static Customer ToCustomerFromCreateDto(this CreateCustomerDto customerDto,int customerTypeId,string appUserId)
    {
        return new Customer()
        {
            Name = customerDto.Name,
            Surname = customerDto.Surname,
            Email = customerDto.Email,
            CustomerNumber=customerDto.CustomerNumber,
            CustomerTypeId=customerTypeId,
            AppUserId=appUserId
        };
    }
    
    public static Customer ToCustomerFromUpdateDto(this UpdateCustomerDto updateCustomerDto)
    {
        return new Customer()
        {
            Name=updateCustomerDto.Name,
            Surname = updateCustomerDto.Surname,
            Email = updateCustomerDto.Email,
            CustomerNumber = updateCustomerDto.CustomerNumber
        };
    }
    
}