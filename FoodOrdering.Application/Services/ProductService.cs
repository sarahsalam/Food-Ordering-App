using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.DTOs.Responses;

namespace FoodOrdering.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<(bool Exists, List<ProductResponse> Data)> GetByCategoryAsync(Guid id, ProductQueryRequest query)
    {
        if (!await _repository.CategoryExistsAsync(id))
        {
            return (false, []);
        }

        var items = await _repository.GetByCategoryAsync(id, query.Search, query.IsActive);
        return (true, items.Select(Map).ToList());
    }

    public async Task<ProductResponse?> GetByIdAsync(Guid id)
    {
        var product = await _repository.GetByIdAsync(id);
        return product == null ? null : Map(product);
    }

    public async Task<(bool Success, string? Error, ProductResponse? Data)> CreateAsync(Guid categoryId,
        CreateProductRequest request)
    {
        if (!await _repository.CategoryExistsAsync(categoryId))
        {
            return (false, "Category was not found.", null);
        }

        var product = new Product
        {
            Id = Guid.NewGuid(),
            CategoryId = categoryId,
            Name = request.Name,
            Description = request.Description,
            BasePrice = request.BasePrice,
            ImageUrl = request.ImageUrl,
            IsActive = request.IsActive,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(product);
        await _repository.SaveChangesAsync();
        return (true, null, Map(product));
    }

    public async Task<(bool Success, string? Error, ProductResponse? Data)> UpdateAsync(Guid id,
        UpdateProductRequest request)
    {
        var product = await _repository.GetTrackedByIdAsync(id);
        if (product == null)
        {
            return (false, "Product was not found.", null);
        }

        product.Name = request.Name;
        product.Description = request.Description;
        product.BasePrice = request.BasePrice;
        product.ImageUrl = request.ImageUrl;
        product.IsActive = request.IsActive;
        product.UpdatedAt = DateTime.UtcNow;
        await _repository.SaveChangesAsync();
        return (true, null, Map(product));
    }

    public async Task<(bool Success, string? Error)> DeleteAsync(Guid id)
    {
        var product = await _repository.GetTrackedByIdAsync(id);
        if (product == null)
        {
            return (false, "Product was not found.");
        }

        product.IsActive = false;
        product.UpdatedAt = DateTime.UtcNow;
        await _repository.SaveChangesAsync();
        return (true, null);
    }

    private static ProductResponse Map(Product product) => new(product.Id, product.CategoryId, product.Name,
        product.Description, product.BasePrice, product.ImageUrl,
        product.IsActive, product.CreatedAt, product.UpdatedAt);
}
