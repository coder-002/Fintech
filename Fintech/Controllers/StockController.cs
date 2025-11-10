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
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var stocks = await _stockRepo.GetAllAsync();
        var stockDto = stocks.Select(s => s.ToStockDto());
        return Ok(stocks);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var stock = await _stockRepo.GetByIdAsync(id);
        ;
        if (stock == null)
        {
            return NotFound();
        }
        return Ok(stock.ToStockDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var stockModel = stockDto.ToStockFromCreateDto();
        await _stockRepo.CreateAsync(stockModel);
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockRequestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var stockModel = await _stockRepo.UpdateAsync(id, updateStockRequestDto);
        if (stockModel == null)
        {
            return NotFound();
        }
        return Ok(stockModel.ToStockDto());
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var stock = await _stockRepo.DeleteAsync(id);
        if (stock == null)
        {
            return NotFound();
        }
        return NoContent();
    }
}