using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Api.Models.Requests;

public record UpdateProductVariantRequest([Required] string Name, decimal Price, bool IsActive);