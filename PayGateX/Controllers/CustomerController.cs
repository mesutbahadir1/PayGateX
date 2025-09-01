using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PayGateX.Dtos.Customer;
using PayGateX.Entities;
using PayGateX.Extensions;
using PayGateX.Interfaces;
using PayGateX.Mappers;

namespace PayGateX.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class CustomerController:ControllerBase
{
    private readonly ICustomerRepository _repository;
    private readonly UserManager<AppUser> _userManager; 
    private readonly ICustomerTypeRepository _customerTypeRepository;
    public CustomerController(UserManager<AppUser> userManager,ICustomerRepository repository,ICustomerTypeRepository customerTypeRepository)
    {
        _userManager = userManager;
        _repository = repository;
        _customerTypeRepository = customerTypeRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCustomers()
    {
        var customers = await _repository.GetAllCustomers();
        var customersDto = customers.Select(x => x.ToCustomerDto());
        return Ok(customersDto);
    }
    
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerById([FromRoute] int id)
    {
        var customer = await _repository.GetCustomerById(id);
        if (customer==null)
        {
            return NotFound("Customer doesn't exist");
        }

        return Ok(customer.ToCustomerDto());
    }
    
    
    [HttpGet("{customerId}/cards")]
    public async Task<IActionResult> GetCustomerCardsById([FromRoute] int customerId)
    {
        var customerCards = await _repository.GetCustomerCards(customerId);
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
        var appUser = await _userManager.FindByNameAsync(userName);

        var isCustomerTypeExist = await _customerTypeRepository.IsCustomerTypeExist(customerTypeId);
        if (!isCustomerTypeExist)
        {
            return BadRequest("Customer type not found");
        }

        var customerModel = createCustomerDto.ToCustomerFromCreateDto(customerTypeId, appUser.Id);

        await _repository.CreateCustomer(customerModel);

        return Ok(customerModel.ToCustomerDto());
    }
    
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer([FromRoute] int id,[FromBody] UpdateCustomerDto updateCustomerDto)
    {
        var customerModel = await _repository.UpdateCustomer(id, updateCustomerDto.ToCustomerFromUpdateDto());
        if (customerModel==null)
        {
            return NotFound("Customer not found");
        }

        return Ok(customerModel.ToCustomerDto());
    }
    
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
    {
        var customerModel = await _repository.DeleteCustomer(id);
        if (customerModel==null)
        {
            return NotFound("Customer not found");
        }

        return Ok(customerModel.ToCustomerDto());
    }
    
}