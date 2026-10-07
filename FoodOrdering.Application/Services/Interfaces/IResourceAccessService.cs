using FoodOrdering.Application.DTOs.Requests;

namespace FoodOrdering.Application.Services.Interfaces;

public interface IResourceAccessService
{
    Task<ResourceAccessResult> CheckAsync(ResourceAccessRequest request);
}
