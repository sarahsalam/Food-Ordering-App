using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Api.Models.Requests;

public class CreateRestaurantRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Required]
    public string Phone { get; set; } = string.Empty;

    [EmailAddress]
    public string? Email { get; set; }

    public string? LogoUrl { get; set; }

    public bool IsActive { get; set; } = true;

}
