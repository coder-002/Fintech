using Fintech.Dtos.Comment;
using Fintech.Interfaces;
using Fintech.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Fintech.Controllers;
[Route("api/comment")]
[ApiController]
public class CommentController: ControllerBase
{
    private readonly ICommentRepository _commentRepository;
    private readonly IStockRepository _stockRepository;

    public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
    {
        _commentRepository = commentRepository;
        _stockRepository = stockRepository;
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var comments = await _commentRepository.GetAllAsync();
        var commentDto = comments.Select(s => s.ToCommentDto());
        return Ok(commentDto);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetById([FromRoute] int id)
    {
        var comment = await _commentRepository.GetByIdAsync(id);

        if (comment == null)
        {
            return NotFound();
        }
        return Ok(comment.ToCommentDto());
    }

    [HttpPost("{stockId:int}")]
    public async Task<IActionResult> Create([FromRoute] int stockId, CreateCommentDto createCommentDto)
    {
        if (!await _stockRepository.StockExists(stockId))
        {
            return BadRequest("Stock does not exist");
        }

        var commentModel = createCommentDto.ToCommentFromCreate(stockId);
        await _commentRepository.CreateAsync(commentModel);
        return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());

    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateCommentDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var comment = await _commentRepository.UpdateAsync(id, updateCommentDto.ToCommentFromUpdate());
        if (comment == null)
        {
          return  NotFound("Comment not found");
        }
        return Ok(comment.ToCommentDto());
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var commentModel = await _commentRepository.DeleteAsync(id);
        if (commentModel == null)
        {
            return NotFound("Comment not found");
        }

        return Ok(commentModel);
    }
    
   
}