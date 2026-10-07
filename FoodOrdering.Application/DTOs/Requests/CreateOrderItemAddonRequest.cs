namespace FoodOrdering.Application.DTOs.Requests;

public record CreateOrderItemAddonRequest(Guid AddonId,int Quantity);
