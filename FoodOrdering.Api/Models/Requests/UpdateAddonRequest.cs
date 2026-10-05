using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Api.Models.Requests;

public record UpdateAddonRequest([Required] string Name, decimal Price, bool IsActive);