using FoodOrdering.Application.DTOs.Responses;

namespace FoodOrdering.Application.Services.Interfaces;

public interface IProductAddonService
{
    Task<(bool Exists, List<ProductAddonResponse> Data)> GetByProductAsync(Guid productId);
    Task<(bool Success, string? Error, ProductAddonResponse? Data)> AddAsync(Guid productId, Guid addonId);
    Task<(bool Success, string? Error)> RemoveAsync(Guid productId, Guid addonId);
}
