using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Application.DTOs.Requests;

public record CreateProductVariantRequest([Required] string Name, decimal Price, bool IsActive = true);
