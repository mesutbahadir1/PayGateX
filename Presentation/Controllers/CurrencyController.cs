using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PayGateX.Dtos.Currency;
using PayGateX.Interfaces;
using PayGateX.Mappers;
using PayGateX.Service.Contracts;

namespace PayGateX.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class CurrencyController:ControllerBase
{
    private readonly ICurrencyService _currencyService;
    
    public CurrencyController(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllCurrencies()
    {
        var currencies = await _currencyService.GetAll();;
        var currenciesDto = currencies.Select(x => x.ToCurrencyDto());
        return Ok(currenciesDto);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCurrencyById([FromRoute]int id)
    {
        var currency = await _currencyService.GetById(id);
        if (currency==null)
            return NotFound("Currency not found");
        
        return Ok(currency.ToCurrencyDto());
    } 
    
    [HttpPost]
    public async Task<IActionResult> CreateCurrency([FromBody]CreateCurrencyDto currencyDto)
    {
        var currency = currencyDto.ToCurrencyFromCreateDto();
        var createCurrency = await _currencyService.Create(currency);
        return CreatedAtAction(nameof(GetCurrencyById), new { id = createCurrency.Id }, createCurrency.ToCurrencyDto());
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCurrency([FromRoute]int id,[FromBody] UpdateCurrencyDto currencyDto)
    {
        var currency = await _currencyService.Update(id, currencyDto.ToCurrencyFromUpdateDto());
        if (currency == null)
            return NotFound("Currency not found");

        return Ok(currency.ToCurrencyDto());
    } 
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCurrency([FromRoute] int id)
    {
        var deletedCurrency = await _currencyService.Delete(id);
        if (deletedCurrency == null)
            return NotFound("Currency not found");
        
        return Ok(deletedCurrency.ToCurrencyDto());
    }
    
    
}