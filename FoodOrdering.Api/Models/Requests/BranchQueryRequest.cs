namespace FoodOrdering.Api.Models.Requests;

public sealed class BranchQueryRequest
{
    public string? Search { get; set; }

    public string? City { get; set; }

    public bool? IsActive { get; set; }
}
