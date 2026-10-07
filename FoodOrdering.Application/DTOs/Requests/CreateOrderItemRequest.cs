namespace FoodOrdering.Application.DTOs.Requests;

public record CreateOrderItemRequest(Guid ProductId,Guid? ProductVariantId,int Quantity);
