using Fintech.Models;

namespace Fintech.Interfaces;

public interface IStockRepository
{
    Task<List<Stock>> GetAllAsync();
}