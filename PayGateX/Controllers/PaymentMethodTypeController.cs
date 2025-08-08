using Microsoft.AspNetCore.Mvc;
using PayGateX.Dtos.PaymentMethodType;
using PayGateX.Interfaces;
using PayGateX.Mappers;

namespace PayGateX.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PaymentMethodTypeController:ControllerBase
{
    private readonly IPaymentMethodTypeRepository _repository;
    public PaymentMethodTypeController(IPaymentMethodTypeRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllPaymentMethodTypes()
    {
        var paymentMethodTypes = await _repository.GetAll();;
        var paymentMethodTypesDto = paymentMethodTypes.Select(x => x.ToPaymentMethodTypeDto());
        return Ok(paymentMethodTypesDto);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPaymentMethodTypeById([FromRoute]int id)
    {
        var paymentMethodType = await _repository.GetById(id);
        if (paymentMethodType==null)
            return NotFound("Payment method status not found");
        
        return Ok(paymentMethodType.ToPaymentMethodTypeDto());
    } 
    
    [HttpPost]
    public async Task<IActionResult> CreatePaymentMethodType([FromBody]CreatePaymentMethodTypeDto paymentMethodType)
    {
        var paymentMethodTypeModel = paymentMethodType.ToPaymentMethodTypeFromCreateDto();
        var createpaymentMethodType = await _repository.Create(paymentMethodTypeModel);
        return CreatedAtAction(nameof(GetPaymentMethodTypeById), new { id = createpaymentMethodType.Id }, createpaymentMethodType.ToPaymentMethodTypeDto());
    }
    
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePaymentMethodType([FromRoute]int id,[FromBody] UpdatePaymentMethodTypeDto paymentDto)
    {
        var paymentMethod = await _repository.Update(id, paymentDto.ToPaymentMethodTypeFromUpdateDto());
        if (paymentMethod == null)
            return NotFound("Payment method not found");

        return Ok(paymentMethod.ToPaymentMethodTypeDto());
    } 
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePaymentMethodStatus([FromRoute] int id)
    {
        var paymentMethod = await _repository.Delete(id);
        if (paymentMethod == null)
            return NotFound("Payment method not found");
        
        return Ok(paymentMethod.ToPaymentMethodTypeDto());
    }
}