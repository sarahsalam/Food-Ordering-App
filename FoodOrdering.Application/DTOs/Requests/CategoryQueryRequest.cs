namespace FoodOrdering.Application.DTOs.Requests;

public sealed class CategoryQueryRequest
{
    public string? Search { get; set; }

    public bool? IsActive { get; set; }
}
