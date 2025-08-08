using PayGateX.Mappers;
using Microsoft.AspNetCore.Mvc;
using PayGateX.Dtos.CustomerType;
using PayGateX.Interfaces;

namespace PayGateX.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerTypeController:ControllerBase
{
    private readonly ICustomerTypeRepository _repository;
        
    public CustomerTypeController(ICustomerTypeRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCustomerTypes()
    {
        var customerTpyes = await _repository.GetAll();
        var customerDtoTypes = customerTpyes.Select(x => x.ToCustomerTypeDto());
        return Ok(customerDtoTypes);
    } 
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerTypesById([FromRoute]int id)
    {
        var customerTpye = await _repository.GetById(id);
        if (customerTpye==null)
            return NotFound("Customer type not found");
        return Ok(customerTpye.ToCustomerTypeDto());
    } 
    
    [HttpPost]
    public async Task<IActionResult> CreateCustomerType([FromBody]CreateCustomerTypeDto customerTypeDto)
    {
        var customerType = customerTypeDto.ToCustomerTypeFromCreateDto();
        var createCustomerType = await _repository.Create(customerType);
        return CreatedAtAction(nameof(GetCustomerTypesById), new { id = createCustomerType.Id }, createCustomerType.ToCustomerTypeDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomerType([FromRoute] int id, [FromBody] UpdateCustomerTypeDto customerTypeDto)
    {
        var updateCustomerType = await _repository.Update(id, customerTypeDto.ToCustomerTypeFromUpdateDto());
        if (updateCustomerType==null)
        {
            return NotFound("Customer type not found");
        }
        return Ok(updateCustomerType.ToCustomerTypeDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomerType([FromRoute] int id)
    {
        var deletedCustomerType = await _repository.Delete(id);
        if (deletedCustomerType == null)
        {
            return NotFound("Customer Type not found");
        }
        return Ok(deletedCustomerType.ToCustomerTypeDto());
    }
}