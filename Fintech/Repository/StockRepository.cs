using Fintech.Data;
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
        return _context.Stocks.ToListAsync();
    }
}