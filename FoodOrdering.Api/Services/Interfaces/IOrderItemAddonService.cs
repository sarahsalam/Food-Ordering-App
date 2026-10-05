using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;

namespace FoodOrdering.Api.Services.Interfaces;

public interface IOrderItemAddonService
{
    Task<(bool Exists, List<OrderItemAddonResponse> Data)> GetByOrderItemAsync(Guid orderItemId);
    Task<OrderItemAddonResponse?> GetByIdAsync(Guid id);

    Task<(bool Success, string? Error, OrderItemAddonResponse? Data)> CreateAsync(Guid orderItemId,
        CreateOrderItemAddonRequest request);

    Task<(bool Success, string? Error, OrderItemAddonResponse? Data)> UpdateAsync(Guid id,
        UpdateOrderItemAddonRequest request);

    Task<(bool Success, string? Error)> DeleteAsync(Guid id);
}