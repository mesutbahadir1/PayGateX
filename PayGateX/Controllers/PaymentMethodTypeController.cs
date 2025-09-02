using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PayGateX.Dtos.PaymentMethodType;
using PayGateX.Interfaces;
using PayGateX.Mappers;
using PayGateX.Service.Contracts;

namespace PayGateX.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class PaymentMethodTypeController:ControllerBase
{
    private readonly IPaymentMethodTypeService _paymentMethodTypeService;
  
    public PaymentMethodTypeController(IPaymentMethodTypeService paymentMethodTypeService)
    {
        _paymentMethodTypeService = paymentMethodTypeService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllPaymentMethodTypes()
    {
        var paymentMethodTypes = await _paymentMethodTypeService.GetAll();;
        var paymentMethodTypesDto = paymentMethodTypes.Select(x => x.ToPaymentMethodTypeDto());
        return Ok(paymentMethodTypesDto);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPaymentMethodTypeById([FromRoute]int id)
    {
        var paymentMethodType = await _paymentMethodTypeService.GetById(id);
        if (paymentMethodType==null)
            return NotFound("Payment method status not found");
        
        return Ok(paymentMethodType.ToPaymentMethodTypeDto());
    } 
    
    [HttpPost]
    public async Task<IActionResult> CreatePaymentMethodType([FromBody]CreatePaymentMethodTypeDto paymentMethodType)
    {
        var paymentMethodTypeModel = paymentMethodType.ToPaymentMethodTypeFromCreateDto();
        var createPaymentMethodType = await _paymentMethodTypeService.Create(paymentMethodTypeModel);
        return CreatedAtAction(nameof(GetPaymentMethodTypeById), new { id = createPaymentMethodType.Id }, createPaymentMethodType.ToPaymentMethodTypeDto());
    }
    
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePaymentMethodType([FromRoute]int id,[FromBody] UpdatePaymentMethodTypeDto paymentDto)
    {
        var paymentMethod = await _paymentMethodTypeService.Update(id, paymentDto.ToPaymentMethodTypeFromUpdateDto());
        if (paymentMethod == null)
            return NotFound("Payment method not found");

        return Ok(paymentMethod.ToPaymentMethodTypeDto());
    } 
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePaymentMethodStatus([FromRoute] int id)
    {
        var paymentMethod = await _paymentMethodTypeService.Delete(id);
        if (paymentMethod == null)
            return NotFound("Payment method not found");
        
        return Ok(paymentMethod.ToPaymentMethodTypeDto());
    }
}