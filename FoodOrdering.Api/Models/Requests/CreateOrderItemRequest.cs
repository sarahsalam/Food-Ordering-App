using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Api.Models.Requests;

public record CreateOrderItemRequest(Guid ProductId,Guid? ProductVariantId,int Quantity);
