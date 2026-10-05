using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;

namespace FoodOrdering.Api.Services.Interfaces;

public interface IProductVariantService
{
    Task<(bool Exists, List<ProductVariantResponse> Data)> GetByProductAsync(Guid productId, ProductQueryRequest query);
    Task<ProductVariantResponse?> GetByIdAsync(Guid id);

    Task<(bool Success, string? Error, ProductVariantResponse? Data)> CreateAsync(Guid productId,
        CreateProductVariantRequest request);

    Task<(bool Success, string? Error, ProductVariantResponse? Data)> UpdateAsync(Guid id,
        UpdateProductVariantRequest request);

    Task<(bool Success, string? Error)> DeleteAsync(Guid id);
}