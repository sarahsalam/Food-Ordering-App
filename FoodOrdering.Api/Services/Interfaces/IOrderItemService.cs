using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;

namespace FoodOrdering.Api.Services.Interfaces;

public interface IOrderItemService
{
    Task<(bool Exists, List<OrderItemResponse> Data)> GetByOrderAsync(Guid orderId);
    Task<OrderItemResponse?> GetByIdAsync(Guid id);

    Task<(bool Success, string? Error, OrderItemResponse? Data)> CreateAsync(Guid orderId,
        CreateOrderItemRequest request);

    Task<(bool Success, string? Error, OrderItemResponse? Data)> UpdateAsync(Guid id, UpdateOrderItemRequest request);
}