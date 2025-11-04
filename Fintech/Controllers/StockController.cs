using Fintech.Data;
using Fintech.Dtos.Stock;
using Fintech.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Fintech.Controllers;

[Route("api/stock")] // this is the api route we define
[ApiController]
public class StockController : ControllerBase
{
    private readonly AppDbContext _context;
    public StockController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var stocks = _context.Stocks.ToList().Select(s => s.ToStockDto());
        return Ok(stocks);
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var stock = _context.Stocks.Find(id);
        if (stock == null)
        {
            return NotFound();
        }

        return Ok(stock.ToStockDto());
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
    {
        var stockModel = stockDto.ToStockFromCreateDto();
        _context.Stocks.Add(stockModel);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockRequestDto)
    {
        var stockModel = _context.Stocks.FirstOrDefault(x => x.Id == id);
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


        _context.SaveChanges();
        return Ok(stockModel.ToStockDto());
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var stock = _context.Stocks.FirstOrDefault(x => x.Id == id);
        if (stock == null)
        {
            return NotFound();
        }

        _context.Stocks.Remove(stock);
        _context.SaveChanges();
        return NoContent();
    }
}