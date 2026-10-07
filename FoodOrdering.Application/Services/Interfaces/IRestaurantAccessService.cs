using FoodOrdering.Application.DTOs.Requests;

namespace FoodOrdering.Application.Services.Interfaces;

public interface IRestaurantAccessService
{
    Task<ResourceAccessResult> CheckAsync(ResourceAccessRequest request);
}
