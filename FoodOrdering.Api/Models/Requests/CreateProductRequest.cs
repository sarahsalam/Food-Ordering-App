using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Api.Models.Requests;

public record CreateProductRequest(
    [Required] string Name,
    string? Description,
    decimal BasePrice,
    string? ImageUrl,
    bool IsActive = true);