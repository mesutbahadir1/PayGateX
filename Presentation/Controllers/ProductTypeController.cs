using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PayGateX.Dtos.ProductType;
using PayGateX.Interfaces;
using PayGateX.Mappers;
using PayGateX.Service.Contracts;

namespace PayGateX.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ProductTypeController:ControllerBase
{
    private readonly IProductTypeService _productTypeService;
    public ProductTypeController(IProductTypeService productTypeService)
    {
        _productTypeService = productTypeService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllProductTypes()
    {
        var productTypes = await _productTypeService.GetAll();
        var productTypesDto = productTypes.Select(x => x.ToProductTypeDto());
        return Ok(productTypesDto);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductTypeById([FromRoute]int id)
    {
        var productType = await _productTypeService.GetById(id);
        if (productType==null)
            return NotFound("Product type not found");
        
        return Ok(productType.ToProductTypeDto());
    } 
    
    [HttpPost]
    public async Task<IActionResult> CreateProductType([FromBody]CreateProductTypeDto createProductTypeDto)
    {
        var productType = createProductTypeDto.ToProductTypeFromCreateDto();
        var createProductType = await _productTypeService.Create(productType);
        return CreatedAtAction(nameof(GetProductTypeById), new { id = createProductType.Id }, createProductType.ToProductTypeDto());
    }
    
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProductType([FromRoute]int id,[FromBody] UpdateProductTypeDto productTypeDto)
    {
        var productType = await _productTypeService.Update(id, productTypeDto.ToProductTypeFromUpdateDto());
        if (productType == null)
            return NotFound("Product type not found");

        return Ok(productType.ToProductTypeDto());
    } 
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductType([FromRoute] int id)
    {
        var deletedProductType= await _productTypeService.Delete(id);
        if (deletedProductType == null)
            return NotFound("Product type not found");
        
        return Ok(deletedProductType.ToProductTypeDto());
    }
    
    
}