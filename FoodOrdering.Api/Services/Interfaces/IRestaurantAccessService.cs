using FoodOrdering.Api.Models.Requests;

namespace FoodOrdering.Api.Services.Interfaces;

public interface IRestaurantAccessService
{
    Task<ResourceAccessResult> CheckAsync(ResourceAccessRequest request);
}
