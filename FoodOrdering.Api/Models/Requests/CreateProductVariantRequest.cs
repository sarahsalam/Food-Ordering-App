using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Api.Models.Requests;

public record CreateProductVariantRequest([Required] string Name, decimal Price, bool IsActive = true);