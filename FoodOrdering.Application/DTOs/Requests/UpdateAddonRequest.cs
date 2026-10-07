using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Application.DTOs.Requests;

public record UpdateAddonRequest([Required] string Name, decimal Price, bool IsActive);
