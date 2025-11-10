using Fintech.Data;
using Fintech.Dtos.Stock;
using Fintech.Interfaces;
using Fintech.Models;
using Microsoft.EntityFrameworkCore;

namespace Fintech.Repository;

public class StockRepository: IStockRepository
{
    private readonly AppDbContext _context;
    public StockRepository(AppDbContext context)
    {
        _context = context;
    }
    public Task<List<Stock>> GetAllAsync()
    {
        return _context.Stocks.Include(c=>c.Comments).ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(int id)
    {
        return await _context.Stocks.Include(c=> c.Comments).FirstOrDefaultAsync(i=> i.Id == id);
    }

    public async Task<Stock> CreateAsync(Stock stockModel)
    {
        await _context.Stocks.AddAsync(stockModel);
        await _context.SaveChangesAsync();
        return stockModel;
    }

    public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
    {
        var existingStock = _context.Stocks.FirstOrDefault(x => x.Id == id);
        if (existingStock != null)
        {
            return null;
        }

        existingStock.Symbol = stockDto.Symbol;
        existingStock.CompanyName = stockDto.CompanyName;
        existingStock.Industry = stockDto.Industry;
        existingStock.LastDiv = stockDto.LastDiv;
        existingStock.Purchase = stockDto.Purchase;
        existingStock.MarketCap = stockDto.MarketCap;
        
        
        await _context.SaveChangesAsync();
        return existingStock;
    }

    public async Task<Stock?> DeleteAsync(int id)
    {
        var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
        if (stockModel != null)
        {
            return null;
        }
        _context.Stocks.Remove(stockModel);
        await _context.SaveChangesAsync();
        return stockModel;
            
    }

    public Task<bool> StockExists(int id)
    {
       return _context.Stocks.AnyAsync(x => x.Id == id);
    }
}