using PayGateX.Data;
using PayGateX.Entities;
using PayGateX.Interfaces;
using Microsoft.EntityFrameworkCore;
/*
namespace PayGateX.Repository;

public class PortfolioRepository:IPortfolioRepository
{
    private readonly ApplicationDBContext _context;
    public PortfolioRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    public async Task<List<Stock>>? GetUserPortfolio(AppUser appUser)
    {
        return await _context.Portfolios.Where(x => x.AppUserId == appUser.Id)
            .Select(u => new Stock
            {
                Id = u.StockId,
                Symbol = u.Stock!.Symbol,
                Industry = u.Stock.Industry,
                LastDiv = u.Stock.LastDiv,
                MarketCap = u.Stock.MarketCap,
                Purchase = u.Stock.Purchase,
                CompanyName = u.Stock.CompanyName
            }).ToListAsync();
    }

    public async Task<Entities.Portfolio> CreatePortfolio(Entities.Portfolio port)
    {
        await _context.Portfolios.AddAsync(port);
        await _context.SaveChangesAsync();
        return port;
    }

    public async Task<Entities.Portfolio> DeletePortfolio(AppUser user, string symbol)
    {
        var portfolio = await _context.Portfolios.FirstOrDefaultAsync(x =>
            x.AppUserId == user.Id && x.Stock.Symbol.ToLower() == symbol.ToLower());

        if (portfolio==null)
            return null;

        _context.Portfolios.Remove(portfolio);
        await _context.SaveChangesAsync();
        return portfolio;
    }
}*/