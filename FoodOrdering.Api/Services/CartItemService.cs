using FoodOrdering.Api.Models.Entities;
using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Repositories.Interfaces;
using FoodOrdering.Api.Services.Interfaces;

namespace FoodOrdering.Api.Services;

public class CartItemService : ICartItemService
{
    private readonly ICartItemRepository _repository;

    public CartItemService(ICartItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<(bool Exists, List<CartItemResponse> Data)> GetByCartAsync(Guid cartId)
    {
        if (!await _repository.CartExistsAsync(cartId))
        {
            return (false, []);
        }

        var items = await _repository.GetByCartAsync(cartId);
        return (true, items.Select(Map).ToList());
    }

    public async Task<CartItemResponse?> GetByIdAsync(Guid id)
    {
        var cartItem = await _repository.GetByIdAsync(id);
        return cartItem == null ? null : Map(cartItem);
    }

    public async Task<(bool Success, string? Error, CartItemResponse? Data)> CreateAsync(Guid cartId,
        CreateCartItemRequest request)
    {
        if (!await _repository.CartExistsAsync(cartId))
        {
            return (false, "Cart was not found.", null);
        }

        if (request.Quantity <= 0)
        {
            return (false, "Quantity must be positive.", null);
        }

        var (product, variant) = await _repository.GetPricingAsync(request.ProductId, request.ProductVariantId);
        if (product == null)
        {
            return (false, "Product was not found.", null);
        }

        if (request.ProductVariantId.HasValue && variant == null)
        {
            return (false, "Variant does not belong to product.", null);
        }

        decimal price = product.BasePrice;
        if (variant != null)
        {
            price = variant.Price;
        }

        var addonIds = request.AddonIds ?? [];
        var addons = await _repository.GetAvailableAddonsAsync(product.Id, addonIds);
        if (addons.Count != addonIds.Count)
        {
            return (false, "Select distinct, active addons linked to this product.", null);
        }

        var cartItem = new CartItem
        {
            Id = Guid.NewGuid(),
            CartId = cartId,
            ProductId = product.Id,
            ProductVariantId = variant?.Id,
            Quantity = request.Quantity,
            UnitPrice = price,
            CreatedAt = DateTime.UtcNow
        };

        foreach (var addon in addons)
        {
            cartItem.Addons.Add(new CartItemAddon { CartItemId = cartItem.Id, AddonId = addon.Id, Addon = addon });
        }
        await _repository.AddAsync(cartItem);
        await _repository.SaveChangesAsync();
        return (true, null, Map(cartItem));
    }

    public async Task<(bool Success, string? Error, CartItemResponse? Data)> UpdateAsync(Guid id,
        UpdateCartItemRequest request)
    {
        var cartItem = await _repository.GetTrackedByIdAsync(id);
        if (cartItem == null)
        {
            return (false, "Cart item was not found.", null);
        }

        if (request.Quantity <= 0)
        {
            return (false, "Quantity must be positive.", null);
        }

        if (request.AddonIds != null)
        {
            var addons = await _repository.GetAvailableAddonsAsync(cartItem.ProductId, request.AddonIds);
            if (addons.Count != request.AddonIds.Count)
            {
                return (false, "Select distinct, active addons linked to this product.", null);
            }
            foreach (var selection in cartItem.Addons.ToList())
            {
                if (!request.AddonIds.Contains(selection.AddonId))
                {
                    cartItem.Addons.Remove(selection);
                }
            }
            foreach (var addon in addons)
            {
                if (!cartItem.Addons.Any(selection => selection.AddonId == addon.Id))
                {
                    cartItem.Addons.Add(new CartItemAddon { CartItemId = cartItem.Id, AddonId = addon.Id, Addon = addon });
                }
            }
        }

        cartItem.Quantity = request.Quantity;
        cartItem.UpdatedAt = DateTime.UtcNow;
        await _repository.SaveChangesAsync();
        return (true, null, Map(cartItem));
    }

    public async Task<(bool Success, string? Error)> DeleteAsync(Guid id)
    {
        var cartItem = await _repository.GetTrackedByIdAsync(id);
        if (cartItem == null)
        {
            return (false, "Cart item was not found.");
        }

        _repository.Remove(cartItem);
        await _repository.SaveChangesAsync();
        return (true, null);
    }

    private static CartItemResponse Map(CartItem cartItem) => new(cartItem.Id, cartItem.CartId, cartItem.ProductId,
        cartItem.ProductVariantId, cartItem.Quantity,
        cartItem.UnitPrice, cartItem.CreatedAt, cartItem.UpdatedAt)
    {
        Addons = cartItem.Addons.Select(item => new CartAddonResponse(
            item.AddonId, item.Addon.Name, item.Addon.Price)).ToList()
    };
}