using Fintech.Data;
using Fintech.Dtos.Stock;
using Fintech.Interfaces;
using Fintech.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fintech.Controllers;

[Route("api/stock")] // this is the api route we define
[ApiController]
public class StockController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IStockRepository _stockRepo;
    public StockController(AppDbContext context, IStockRepository stockRepo)
    {
        _stockRepo = stockRepo;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var stocks = await _stockRepo.GetAllAsync();
        var stockDto = stocks.Select(s => s.ToStockDto());
        return Ok(stocks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var stock = await _context.Stocks.FindAsync(id);
        if (stock == null)
        {
            return NotFound();
        }
        return Ok(stock.ToStockDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
    {
        var stockModel = stockDto.ToStockFromCreateDto();
       await _context.Stocks.AddAsync(stockModel);
       await  _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockRequestDto)
    {
        var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
        if (stockModel == null)
        {
            return NotFound();
        }

        stockModel.Symbol = updateStockRequestDto.Symbol;
        stockModel.CompanyName = updateStockRequestDto.CompanyName;
        stockModel.Industry = updateStockRequestDto.Industry;
        stockModel.LastDiv = updateStockRequestDto.LastDiv;
        stockModel.Purchase = updateStockRequestDto.Purchase;
        stockModel.MarketCap = updateStockRequestDto.MarketCap;


        await _context.SaveChangesAsync();
        return Ok(stockModel.ToStockDto());
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
        if (stock == null)
        {
            return NotFound();
        }
        _context.Stocks.Remove(stock);
       await _context.SaveChangesAsync();
        return NoContent();
    }
}