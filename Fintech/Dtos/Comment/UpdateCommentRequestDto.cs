using System.ComponentModel.DataAnnotations;

namespace Fintech.Dtos.Comment;

public class UpdateCommentRequestDto
{
    [Required]
    [MinLength(5, ErrorMessage = "Title must be at least 5 characters long.")]
    [MaxLength(280, ErrorMessage = "Title should not be more than 280 characters.")]
    public string Title{get;set;} = string.Empty;

    [Required]
    [MinLength(5, ErrorMessage = "Content must be at least 5 characters long.")]
    [MaxLength(280, ErrorMessage = "Content should not be more than 280 characters.")]
    public string Content{get;set;} = string.Empty;
}