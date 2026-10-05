namespace FoodOrdering.Api.Models.Requests;

public sealed class RestaurantQueryRequest
{
    public string? Search { get; set; }

    public bool? IsActive { get; set; }
}
