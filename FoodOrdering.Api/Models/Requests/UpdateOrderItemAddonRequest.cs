using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Api.Models.Requests;

public record UpdateOrderItemAddonRequest(int Quantity);
