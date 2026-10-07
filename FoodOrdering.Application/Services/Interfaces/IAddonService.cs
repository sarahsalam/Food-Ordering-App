using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.DTOs.Responses;

namespace FoodOrdering.Application.Services.Interfaces;

public interface IAddonService
{
    Task<List<AddonResponse>> GetAsync(ProductQueryRequest query);
    Task<AddonResponse?> GetByIdAsync(Guid id);
    Task<(bool Success, string? Error, AddonResponse? Data)> CreateAsync(
        Guid restaurantId,
        CreateAddonRequest request);
    Task<(bool Success, string? Error, AddonResponse? Data)> UpdateAsync(Guid id, UpdateAddonRequest request);
    Task<(bool Success, string? Error)> DeleteAsync(Guid id);
}
