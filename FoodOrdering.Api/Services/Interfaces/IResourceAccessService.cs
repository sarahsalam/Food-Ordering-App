using FoodOrdering.Api.Models.Requests;

namespace FoodOrdering.Api.Services.Interfaces;

public interface IResourceAccessService
{
    Task<ResourceAccessResult> CheckAsync(ResourceAccessRequest request);
}
