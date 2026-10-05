using FoodOrdering.Api.Models.Entities;
using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Repositories.Interfaces;
using FoodOrdering.Api.Services.Interfaces;

namespace FoodOrdering.Api.Services;

public class AddonService : IAddonService
{
    private readonly IAddonRepository _repository;

    public AddonService(IAddonRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<AddonResponse>> GetAsync(ProductQueryRequest query) =>
        (await _repository.GetAsync(query.Search, query.IsActive)).Select(Map).ToList();

    public async Task<AddonResponse?> GetByIdAsync(Guid id)
    {
        var addon = await _repository.GetByIdAsync(id);
        return addon == null ? null : Map(addon);
    }

    public async Task<(bool Success, string? Error, AddonResponse? Data)> CreateAsync(
        Guid restaurantId,
        CreateAddonRequest request)
    {
        var addon = new Addon
        {
            Id = Guid.NewGuid(),
            RestaurantId = restaurantId,
            Name = request.Name,
            Price = request.Price,
            IsActive = request.IsActive,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(addon);
        await _repository.SaveChangesAsync();
        return (true, null, Map(addon));
    }

    public async Task<(bool Success, string? Error, AddonResponse? Data)> UpdateAsync(Guid id,
        UpdateAddonRequest request)
    {
        var addon = await _repository.GetTrackedByIdAsync(id);
        if (addon == null)
        {
            return (false, "Addon was not found.", null);
        }

        addon.Name = request.Name;
        addon.Price = request.Price;
        addon.IsActive = request.IsActive;
        addon.UpdatedAt = DateTime.UtcNow;
        await _repository.SaveChangesAsync();
        return (true, null, Map(addon));
    }

    public async Task<(bool Success, string? Error)> DeleteAsync(Guid id)
    {
        var addon = await _repository.GetTrackedByIdAsync(id);
        if (addon == null)
        {
            return (false, "Addon was not found.");
        }

        addon.IsActive = false;
        addon.UpdatedAt = DateTime.UtcNow;
        await _repository.SaveChangesAsync();
        return (true, null);
    }

    private static AddonResponse Map(Addon addon) => new(addon.Id, addon.Name, addon.Price, addon.IsActive,
        addon.CreatedAt, addon.UpdatedAt) { RestaurantId = addon.RestaurantId };
}