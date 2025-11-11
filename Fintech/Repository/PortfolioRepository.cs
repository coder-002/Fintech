using Fintech.Data;
using Fintech.Interfaces;
using Fintech.Models;
using Microsoft.EntityFrameworkCore;

namespace Fintech.Repository;

public class PortfolioRepository: IPortfolioRepository
{
    private readonly AppDbContext _context;
    public PortfolioRepository(AppDbContext context)
    {
        _context = context;
    }
    public Task<List<Stock>> GetUserPortfolio(AppUser user)
    {
        return _context.Portfolios.Where(u => u.AppUserId == user.Id)
            .Select(stock => new Stock
            {
                Id = stock.StockId,
                Symbol = stock.Stock.Symbol,
                CompanyName = stock.Stock.CompanyName,
                Purchase = stock.Stock.Purchase,
                LastDiv = stock.Stock.LastDiv,
                Industry = stock.Stock.Industry,
                MarketCap = stock.Stock.MarketCap
            }).ToListAsync();
    }
}