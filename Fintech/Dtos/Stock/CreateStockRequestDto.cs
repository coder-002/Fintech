using System.ComponentModel.DataAnnotations;

namespace Fintech.Dtos.Stock;

public class CreateStockRequestDto
{
    [Required]
    [MaxLength(6, ErrorMessage = "Symbol should not be more than 6 characters.")]
    public string Symbol { get; set; } = string.Empty;

    [Required]
    [MinLength(6, ErrorMessage = "Company Name should be more than 6 characters.")]
    public string CompanyName { get; set; } = string.Empty;

    [Required]
   [Range(1, 100000000)]
    public decimal Purchase { get; set; }

    [Required]
    [Range(0.001, 100)]
    public decimal LastDiv { get; set; }

    [Required]
    [MaxLength(10, ErrorMessage = "Industry should not be more than 10 characters.")]
    public string Industry { get; set; } = string.Empty;

    [Range(1, 10000000000000)]
    public long MarketCap { get; set; }
}