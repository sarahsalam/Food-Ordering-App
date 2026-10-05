using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Api.Models.Requests;

public class UpdateCategoryRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsActive { get; set; }
}
