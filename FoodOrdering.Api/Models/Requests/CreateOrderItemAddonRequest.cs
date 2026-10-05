using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Api.Models.Requests;

public record CreateOrderItemAddonRequest(Guid AddonId,int Quantity);
