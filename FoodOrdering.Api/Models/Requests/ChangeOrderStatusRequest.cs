using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Api.Models.Requests;

public class ChangeOrderStatusRequest
{
    [Required]
    public string Status { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? Note { get; set; }
}
