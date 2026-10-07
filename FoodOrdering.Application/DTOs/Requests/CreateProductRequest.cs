using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Application.DTOs.Requests;

public record CreateProductRequest(
    [Required] string Name,
    string? Description,
    decimal BasePrice,
    string? ImageUrl,
    bool IsActive = true);
