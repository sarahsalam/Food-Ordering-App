using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.DTOs.Responses;

namespace FoodOrdering.Application.Services.Interfaces;

public interface ICheckoutService
{
    Task<(bool Success, string? Error, OrderResponse? Data)> CheckoutAsync(
        Guid userId, CheckoutRequest request, CancellationToken cancellationToken = default);
}
