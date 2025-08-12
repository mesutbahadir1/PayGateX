using Microsoft.AspNetCore.Mvc;
using PayGateX.Dtos.Currency;
using PayGateX.Interfaces;
using PayGateX.Mappers;

namespace PayGateX.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class CurrencyController:ControllerBase
{
    private readonly ICurrencyRepository _repository;
    public CurrencyController(ICurrencyRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllCurrencies()
    {
        var currencies = await _repository.GetAll();;
        var currenciesDto = currencies.Select(x => x.ToCurrencyDto());
        return Ok(currenciesDto);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCurrencyById([FromRoute]int id)
    {
        var currency = await _repository.GetById(id);
        if (currency==null)
            return NotFound("Currency not found");
        
        return Ok(currency.ToCurrencyDto());
    } 
    
    [HttpPost]
    public async Task<IActionResult> CreateCurrency([FromBody]CreateCurrencyDto currencyDto)
    {
        var currency = currencyDto.ToCurrencyFromCreateDto();
        var createCurrency = await _repository.Create(currency);
        return CreatedAtAction(nameof(GetCurrencyById), new { id = createCurrency.Id }, createCurrency.ToCurrencyDto());
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCurrency([FromRoute]int id,[FromBody] UpdateCurrencyDto currencyDto)
    {
        var currency = await _repository.Update(id, currencyDto.ToCurrencyFromUpdateDto());
        if (currency == null)
            return NotFound("Currency not found");

        return Ok(currency.ToCurrencyDto());
    } 
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCurrency([FromRoute] int id)
    {
        var deletedCurrency = await _repository.Delete(id);
        if (deletedCurrency == null)
            return NotFound("Currency not found");
        
        return Ok(deletedCurrency.ToCurrencyDto());
    }
    
    
}