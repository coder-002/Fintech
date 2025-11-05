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

    public CommentController(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    [HttpGet]
    public async Task<ActionResult> GetALl()
    {
        var comments = await _commentRepository.GetAllAsync();
        var commentDto = comments.Select(s => s.ToCommentDto());
        return Ok(commentDto);
    }

    public async Task<ActionResult> GetByIdAsync([FromRoute] int id)
    {
        var comment = await _commentRepository.GetByIdAsync(id);

        if (comment == null)
        {
            return NotFound();
        }
        return Ok(comment.ToCommentDto());
    }
   
}