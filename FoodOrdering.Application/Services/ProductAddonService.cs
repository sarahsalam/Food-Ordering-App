using FoodOrdering.Application.DTOs.Responses;

namespace FoodOrdering.Application.Services;

public class ProductAddonService : IProductAddonService
{
    private readonly IProductAddonRepository _repository;

    public ProductAddonService(IProductAddonRepository repository)
    {
        _repository = repository;
    }

    public async Task<(bool Exists, List<ProductAddonResponse> Data)> GetByProductAsync(Guid productId)
    {
        if (!await _repository.ProductExistsAsync(productId))
        {
            return (false, []);
        }

        var productAddons = await _repository.GetByProductAsync(productId);
        return (true, productAddons.Select(Map).ToList());
    }

    public async Task<(bool Success, string? Error, ProductAddonResponse? Data)> AddAsync(Guid productId, Guid addonId)
    {
        if (!await _repository.ProductExistsAsync(productId))
        {
            return (false, "Product was not found.", null);
        }

        var addon = await _repository.GetAddonAsync(addonId);
        if (addon == null)
        {
            return (false, "Addon was not found.", null);
        }

        var restaurantId = await _repository.GetProductRestaurantIdAsync(productId);
        if (addon.RestaurantId == null || addon.RestaurantId != restaurantId)
        {
            return (false, "Addon must belong to the same restaurant as the product.", null);
        }

        if (await _repository.ExistsAsync(productId, addonId))
        {
            return (false, "CONFLICT: Addon is already assigned.", null);
        }

        await _repository.AddAsync(new ProductAddon
        {
            ProductId = productId,
            AddonId = addonId
        });
        await _repository.SaveChangesAsync();
        return (true, null, Map(new ProductAddon
        {
            ProductId = productId,
            AddonId = addonId,
            Addon = addon
        }));
    }

    public async Task<(bool Success, string? Error)> RemoveAsync(Guid productId, Guid addonId)
    {
        var productAddon = await _repository.GetTrackedAsync(productId, addonId);
        if (productAddon == null)
        {
            return (false, "Assignment was not found.");
        }

        _repository.Remove(productAddon);
        await _repository.SaveChangesAsync();
        return (true, null);
    }

    private static ProductAddonResponse Map(ProductAddon productAddon) =>
        new(productAddon.ProductId, productAddon.AddonId, Map(productAddon.Addon));

    private static AddonResponse Map(Addon addon) => new(addon.Id, addon.Name, addon.Price, addon.IsActive,
        addon.CreatedAt, addon.UpdatedAt) { RestaurantId = addon.RestaurantId };
}
