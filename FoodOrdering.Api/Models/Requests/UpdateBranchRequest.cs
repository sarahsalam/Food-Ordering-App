using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Api.Models.Requests;

public class UpdateBranchRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Address { get; set; } = string.Empty;

    [Required]
    public string City { get; set; } = string.Empty;

    public string? Phone { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public bool IsActive { get; set; }
}
