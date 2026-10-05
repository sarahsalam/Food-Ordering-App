using FoodOrdering.Api.Data;
using FoodOrdering.Api.Models.Entities;
using FoodOrdering.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodOrdering.Api.Repositories;

public class ProductVariantRepository : IProductVariantRepository
{
    private readonly AppDbContext _context;

    public ProductVariantRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<bool> ProductExistsAsync(Guid id) => _context.Products.AnyAsync(product => product.Id == id);

    public Task<List<ProductVariant>> GetByProductAsync(Guid id, string? search, bool? isActive)
    {
        var query = _context.ProductVariants.AsNoTracking().Where(variant => variant.ProductId == id);
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(productVariant => EF.Functions.ILike(productVariant.Name, $"%{search.Trim()}%"));
        }

        if (isActive.HasValue)
        {
            query = query.Where(productVariant => productVariant.IsActive == isActive);
        }

        return query.ToListAsync();
    }

    public Task<ProductVariant?> GetByIdAsync(Guid id) =>
        _context.ProductVariants.AsNoTracking().FirstOrDefaultAsync(variant => variant.Id == id);

    public Task<ProductVariant?> GetTrackedByIdAsync(Guid id) =>
        _context.ProductVariants.FirstOrDefaultAsync(variant => variant.Id == id);

    public Task AddAsync(ProductVariant productVariant) => _context.ProductVariants.AddAsync(productVariant).AsTask();
    public Task SaveChangesAsync() => _context.SaveChangesAsync();
}