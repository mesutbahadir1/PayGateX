/*using PayGateX.Entities;
using PayGateX.Extensions;
using PayGateX.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PayGateX.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PortfolioController:ControllerBase
{
    private readonly UserManager<AppUser> _userManager; 
    private readonly IStockRepository _stockRepository;
    private readonly IPortfolioRepository _portfolioRepository;
    public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepository,IPortfolioRepository portfolioRepository)
    {
        _userManager = userManager;
        _stockRepository = stockRepository;
        _portfolioRepository = portfolioRepository;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserPortfolio()
    {
        var userName = User.GetUserName();
        var appUser = await _userManager.FindByNameAsync(userName);
        var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
        return Ok(userPortfolio);

    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreatePortfolio(string symbol)
    {
        var userName = User.GetUserName();
        var appUser = await _userManager.FindByNameAsync(userName);
        var stock = await _stockRepository.GetStockBySymbolAsync(symbol);

        if (stock == null)
            return BadRequest("Stock not found");

        var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
        if (userPortfolio.Any(e=>e.Symbol.ToLower()==symbol.ToLower()))
            return BadRequest("Cannot add same stock to same user.");

        var portfolioModel = new Portfolio
        {
            StockId = stock.Id,
            AppUserId = appUser.Id
        };

        await _portfolioRepository.CreatePortfolio(portfolioModel);
        if (portfolioModel == null)
            return StatusCode(500, "Couldnt create");

        return Ok(portfolioModel);
    }
    
    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeletePortfolio(string symbol)
    {
        var userName = User.GetUserName();
        var appUser = await _userManager.FindByNameAsync(userName);
        var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);

        var filteredStock = userPortfolio.Where(s => s.Symbol.ToLower() == symbol.ToLower()).ToList();
        if (filteredStock.Count() == 1)
        {
            await _portfolioRepository.DeletePortfolio(appUser, symbol);
        }
        else
        {
            return BadRequest("Stock is not in your portfolio");
        }

        return Ok("Stock is deleted succesfully");
    }
}*/