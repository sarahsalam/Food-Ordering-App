using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Application.DTOs.Requests;

public record CreateAddonRequest([Required] string Name, decimal Price, bool IsActive = true);
