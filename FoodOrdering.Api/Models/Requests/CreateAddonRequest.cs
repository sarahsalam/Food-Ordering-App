using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Api.Models.Requests;

public record CreateAddonRequest([Required] string Name, decimal Price, bool IsActive = true);
