using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;

namespace FoodOrdering.Api.Services.Interfaces;

public interface ICartService
{
    Task<CartResponse?> GetByIdAsync(Guid id);
    Task<(bool Success, string? Error, CartResponse? Data)> CreateAsync(CreateCartRequest request);
    Task<(bool Success, string? Error, CartResponse? Data)> UpdateAsync(Guid id, UpdateCartRequest request);
    Task<(bool Success, string? Error)> DeleteAsync(Guid id);
}