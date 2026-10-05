using FoodOrdering.Api.Data;
using FoodOrdering.Api.Models.Entities;
using FoodOrdering.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodOrdering.Api.Repositories;

public class ResourceAccessRepository : IResourceAccessRepository
{
    private readonly AppDbContext _context;

    public ResourceAccessRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<bool> OwnsCartAsync(Guid cartId, Guid userId)
    {
        return _context.Carts.AnyAsync(cart => cart.Id == cartId && cart.UserId == userId);
    }

    public Task<bool> OwnsCartItemAsync(Guid itemId, Guid userId)
    {
        return _context.CartItems.AnyAsync(item => item.Id == itemId && item.Cart.UserId == userId);
    }

    public Task<bool> OwnsOrderAsync(Guid orderId, Guid userId)
    {
        return _context.Orders.AnyAsync(order => order.Id == orderId && order.UserId == userId);
    }

    public Task<bool> OwnsOrderItemAsync(Guid itemId, Guid userId)
    {
        return _context.OrderItems.AnyAsync(item => item.Id == itemId && item.Order.UserId == userId);
    }

    public Task<bool> OwnsOrderItemAddonAsync(Guid addonId, Guid userId)
    {
        return _context.OrderItemAddons.AnyAsync(item => item.Id == addonId && item.OrderItem.Order.UserId == userId);
    }

    public Task<Guid?> GetAddonRestaurantIdAsync(Guid id)
    {
        return _context.Addons.Where(addon => addon.Id == id)
            .Select(addon => addon.RestaurantId).SingleOrDefaultAsync();
    }

    public Task<Guid?> GetBranchRestaurantIdAsync(Guid id)
    {
        return _context.RestaurantBranches.Where(branch => branch.Id == id)
            .Select(branch => (Guid?)branch.RestaurantId).SingleOrDefaultAsync();
    }

    public Task<Guid?> GetCategoryRestaurantIdAsync(Guid id)
    {
        return _context.Categories.Where(category => category.Id == id)
            .Select(category => (Guid?)category.RestaurantId).SingleOrDefaultAsync();
    }

    public Task<Guid?> GetProductRestaurantIdAsync(Guid id)
    {
        return _context.Products.Where(product => product.Id == id)
            .Select(product => (Guid?)product.Category.RestaurantId).SingleOrDefaultAsync();
    }

    public Task<Guid?> GetVariantRestaurantIdAsync(Guid id)
    {
        return _context.ProductVariants.Where(variant => variant.Id == id)
            .Select(variant => (Guid?)variant.Product.Category.RestaurantId).SingleOrDefaultAsync();
    }

    public Task<Restaurant?> GetRestaurantAsync(Guid? id)
    {
        return _context.Restaurants.AsNoTracking().SingleOrDefaultAsync(restaurant => restaurant.Id == id);
    }

}
