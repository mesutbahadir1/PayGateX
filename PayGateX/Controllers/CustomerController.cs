using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PayGateX.Dtos.Customer;
using PayGateX.Entities;
using PayGateX.Extensions;
using PayGateX.Interfaces;
using PayGateX.Mappers;
using PayGateX.Service.Contracts;

namespace PayGateX.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class CustomerController:ControllerBase
{
    private readonly ICustomerService _customerService;
    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCustomers()
    {
        var customers = await _customerService.GetAllCustomers();
        var customersDto = customers.Select(x => x.ToCustomerDto());
        return Ok(customersDto);
    }
    
    //[Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerById([FromRoute] int id)
    {
        var customer = await _customerService.GetCustomerById(id);
        if (customer==null)
        {
            return NotFound("Customer doesn't exist");
        }

        return Ok(customer.ToCustomerDto());
    }
    
    
    [HttpGet("{customerId}/cards")]
    public async Task<IActionResult> GetCustomerCardsById([FromRoute] int customerId)
    {
        var customerCards = await _customerService.GetCustomerCards(customerId);
        if (customerCards==null)
        {
            return NotFound("Customer doesn't exist or there is no card for this customer.");
        }

        var customerCardsDto = customerCards.Select(x => x.ToCardDto());
        return Ok(customerCardsDto);
    }
    
    
    
    
    [Authorize]
    [HttpPost("{customerTypeId}")]
    public async Task<IActionResult> CreateCustomer([FromRoute] int customerTypeId,[FromBody] CreateCustomerDto createCustomerDto)
    {
        var userName = User.GetUserName();
        var customerModel= await _customerService.CreateCustomer(customerTypeId,createCustomerDto,userName);
        if (customerModel==null)
        {
            return BadRequest("Customer not created. Check the information.");
        }

        return Ok(customerModel.ToCustomerDto());
    }
    
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer([FromRoute] int id,[FromBody] UpdateCustomerDto updateCustomerDto)
    {
        var customerModel = await _customerService.UpdateCustomer(id, updateCustomerDto.ToCustomerFromUpdateDto());
        if (customerModel==null)
        {
            return NotFound("Customer not found");
        }

        return Ok(customerModel.ToCustomerDto());
    }
    
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
    {
        var customerModel = await _customerService.DeleteCustomer(id);
        if (customerModel==null)
        {
            return NotFound("Customer not found");
        }

        return Ok(customerModel.ToCustomerDto());
    }
    
}