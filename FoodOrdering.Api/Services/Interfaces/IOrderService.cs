using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;

namespace FoodOrdering.Api.Services.Interfaces;

public interface IOrderService
{
    Task<List<OrderResponse>> GetAsync(string? status, Guid? userId);
    Task<OrderResponse?> GetByIdAsync(Guid id);
    Task<(bool Success, string? Error, OrderResponse? Data)> CreateAsync(CreateOrderRequest request);
    Task<(bool Success, string? Error, OrderResponse? Data)> UpdateAsync(Guid id, UpdateOrderRequest request);
}