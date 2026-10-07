using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Application.DTOs.Requests;

public record UpdateProductVariantRequest([Required] string Name, decimal Price, bool IsActive);
