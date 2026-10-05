using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;

namespace FoodOrdering.Api.Services.Interfaces;

public interface ICartItemService
{
    Task<(bool Exists, List<CartItemResponse> Data)> GetByCartAsync(Guid cartId);
    Task<CartItemResponse?> GetByIdAsync(Guid id);
    Task<(bool Success, string? Error, CartItemResponse? Data)> CreateAsync(Guid cartId, CreateCartItemRequest request);
    Task<(bool Success, string? Error, CartItemResponse? Data)> UpdateAsync(Guid id, UpdateCartItemRequest request);
    Task<(bool Success, string? Error)> DeleteAsync(Guid id);
}