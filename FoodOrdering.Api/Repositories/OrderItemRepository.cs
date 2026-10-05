using FoodOrdering.Api.Data;
using FoodOrdering.Api.Models.Entities;
using FoodOrdering.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodOrdering.Api.Repositories;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly AppDbContext _context;

    public OrderItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<bool> OrderExistsAsync(Guid id) => _context.Orders.AnyAsync(order => order.Id == id);

    public Task<List<OrderItem>> GetByOrderAsync(Guid id) =>
        _context.OrderItems.AsNoTracking().Where(orderItem => orderItem.OrderId == id).ToListAsync();

    public Task<OrderItem?> GetByIdAsync(Guid id) =>
        _context.OrderItems.AsNoTracking().FirstOrDefaultAsync(orderItem => orderItem.Id == id);

    public Task<OrderItem?> GetTrackedByIdAsync(Guid id) =>
        _context.OrderItems.FirstOrDefaultAsync(orderItem => orderItem.Id == id);

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

    public Task AddAsync(OrderItem orderItem) => _context.OrderItems.AddAsync(orderItem).AsTask();
    public Task SaveChangesAsync() => _context.SaveChangesAsync();
}