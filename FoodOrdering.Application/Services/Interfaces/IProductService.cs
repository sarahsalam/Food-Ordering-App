using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.DTOs.Responses;

namespace FoodOrdering.Application.Services.Interfaces;

public interface IProductService
{
    Task<(bool Exists, List<ProductResponse> Data)> GetByCategoryAsync(Guid categoryId, ProductQueryRequest query);
    Task<ProductResponse?> GetByIdAsync(Guid id);

    Task<(bool Success, string? Error, ProductResponse? Data)> CreateAsync(Guid categoryId,
        CreateProductRequest request);

    Task<(bool Success, string? Error, ProductResponse? Data)> UpdateAsync(Guid id, UpdateProductRequest request);
    Task<(bool Success, string? Error)> DeleteAsync(Guid id);
}
