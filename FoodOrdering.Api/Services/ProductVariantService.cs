using FoodOrdering.Api.Models.Entities;
using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Repositories.Interfaces;
using FoodOrdering.Api.Services.Interfaces;

namespace FoodOrdering.Api.Services;

public class ProductVariantService : IProductVariantService
{
    private readonly IProductVariantRepository _repository;

    public ProductVariantService(IProductVariantRepository repository)
    {
        _repository = repository;
    }

    public async Task<(bool Exists, List<ProductVariantResponse> Data)>
        GetByProductAsync(Guid id, ProductQueryRequest query)
    {
        if (!await _repository.ProductExistsAsync(id))
        {
            return (false, []);
        }

        var items = await _repository.GetByProductAsync(id, query.Search, query.IsActive);
        return (true, items.Select(Map).ToList());
    }

    public async Task<ProductVariantResponse?> GetByIdAsync(Guid id)
    {
        var productVariant = await _repository.GetByIdAsync(id);
        return productVariant == null ? null : Map(productVariant);
    }

    public async Task<(bool Success, string? Error, ProductVariantResponse? Data)> CreateAsync(Guid productId,
        CreateProductVariantRequest request)
    {
        if (!await _repository.ProductExistsAsync(productId))
        {
            return (false, "Product was not found.", null);
        }

        var productVariant = new ProductVariant
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            Name = request.Name,
            Price = request.Price,
            IsActive = request.IsActive,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(productVariant);
        await _repository.SaveChangesAsync();
        return (true, null, Map(productVariant));
    }

    public async Task<(bool Success, string? Error, ProductVariantResponse? Data)> UpdateAsync(Guid id,
        UpdateProductVariantRequest request)
    {
        var productVariant = await _repository.GetTrackedByIdAsync(id);
        if (productVariant == null)
        {
            return (false, "Variant was not found.", null);
        }

        productVariant.Name = request.Name;
        productVariant.Price = request.Price;
        productVariant.IsActive = request.IsActive;
        productVariant.UpdatedAt = DateTime.UtcNow;
        await _repository.SaveChangesAsync();
        return (true, null, Map(productVariant));
    }

    public async Task<(bool Success, string? Error)> DeleteAsync(Guid id)
    {
        var productVariant = await _repository.GetTrackedByIdAsync(id);
        if (productVariant == null)
        {
            return (false, "Variant was not found.");
        }

        productVariant.IsActive = false;
        productVariant.UpdatedAt = DateTime.UtcNow;
        await _repository.SaveChangesAsync();
        return (true, null);
    }

    private static ProductVariantResponse Map(ProductVariant productVariant) =>
        new(productVariant.Id, productVariant.ProductId, productVariant.Name, productVariant.Price,
            productVariant.IsActive, productVariant.CreatedAt, productVariant.UpdatedAt);
}