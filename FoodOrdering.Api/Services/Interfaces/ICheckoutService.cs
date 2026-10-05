using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;

namespace FoodOrdering.Api.Services.Interfaces;

public interface ICheckoutService
{
    Task<(bool Success, string? Error, OrderResponse? Data)> CheckoutAsync(
        Guid userId, CheckoutRequest request, CancellationToken cancellationToken = default);
}
