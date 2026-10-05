using FoodOrdering.Api.Data;
using FoodOrdering.Api.Models.Entities;
using FoodOrdering.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodOrdering.Api.Repositories;

public class CartItemRepository : ICartItemRepository
{
    private readonly AppDbContext _context;

    public CartItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<bool> CartExistsAsync(Guid id) => _context.Carts.AnyAsync(cart => cart.Id == id);

    public Task<List<CartItem>> GetByCartAsync(Guid id) =>
        _context.CartItems.Include(item => item.Addons).ThenInclude(item => item.Addon).AsNoTracking().Where(cartItem => cartItem.CartId == id).ToListAsync();

    public Task<CartItem?> GetByIdAsync(Guid id) =>
        _context.CartItems.Include(item => item.Addons).ThenInclude(item => item.Addon).AsNoTracking().FirstOrDefaultAsync(cartItem => cartItem.Id == id);

    public Task<CartItem?> GetTrackedByIdAsync(Guid id) =>
        _context.CartItems.Include(item => item.Addons).ThenInclude(item => item.Addon).FirstOrDefaultAsync(cartItem => cartItem.Id == id);

    public async Task<(Product? Product, ProductVariant? Variant)> GetPricingAsync(Guid productId,
        Guid? productVariantId)
    {
        var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(product => product.Id == productId);
        var variant = productVariantId.HasValue
            ? await _context.ProductVariants.AsNoTracking().FirstOrDefaultAsync(variant =>
                variant.Id == productVariantId && variant.ProductId == productId)
            : null;
        return (product, variant);
    }

    public Task<List<Addon>> GetAvailableAddonsAsync(Guid productId, List<Guid> addonIds)
    {
        return _context.ProductAddons.Where(item => item.ProductId == productId &&
            addonIds.Contains(item.AddonId) && item.Addon.RestaurantId == item.Product.Category.RestaurantId && item.Addon.IsActive && item.Addon.Price >= 0)
            .Select(item => item.Addon).ToListAsync();
    }

    public Task AddAsync(CartItem cartItem) => _context.CartItems.AddAsync(cartItem).AsTask();
    public void Remove(CartItem cartItem) => _context.CartItems.Remove(cartItem);
    public Task SaveChangesAsync() => _context.SaveChangesAsync();
}