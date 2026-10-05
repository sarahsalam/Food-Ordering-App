using FoodOrdering.Api.Models.Entities;
using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Repositories.Interfaces;
using FoodOrdering.Api.Services.Interfaces;

namespace FoodOrdering.Api.Services;

public class RestaurantAccessService : IRestaurantAccessService
{
    private readonly IResourceAccessRepository _repository;

    public RestaurantAccessService(IResourceAccessRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResourceAccessResult> CheckAsync(ResourceAccessRequest request)
    {
        Guid? restaurantId;
        switch (request.Controller)
        {
            case "Addons":
                if (request.IsRead && request.Id == Guid.Empty)
                {
                    return ResourceAccessResult.Allowed;
                }
                if (request.Id == Guid.Empty)
                {
                    restaurantId = request.RestaurantId;
                }
                else
                {
                    restaurantId = await _repository.GetAddonRestaurantIdAsync(request.Id);
                }
                break;

            case "Restaurants":
                if (request.Id == Guid.Empty)
                {
                    return ResourceAccessResult.Allowed;
                }
                restaurantId = request.Id;
                break;

            case "RestaurantBranches":
                restaurantId = request.RestaurantId;
                if (request.RestaurantId == Guid.Empty)
                {
                    restaurantId = await _repository.GetBranchRestaurantIdAsync(request.Id);
                }
                break;

            case "Categories":
                restaurantId = request.RestaurantId;
                if (request.RestaurantId == Guid.Empty)
                {
                    restaurantId = await _repository.GetCategoryRestaurantIdAsync(request.Id);
                }
                break;

            case "Products":
                if (request.CategoryId != Guid.Empty)
                {
                    restaurantId = await _repository.GetCategoryRestaurantIdAsync(request.CategoryId);
                }
                else
                {
                    restaurantId = await _repository.GetProductRestaurantIdAsync(request.Id);
                }
                break;

            case "ProductVariants":
            case "ProductAddons":
                if (request.ProductId != Guid.Empty)
                {
                    restaurantId = await _repository.GetProductRestaurantIdAsync(request.ProductId);
                }
                else
                {
                    restaurantId = await _repository.GetVariantRestaurantIdAsync(request.Id);
                }
                break;

            default:
                return ResourceAccessResult.Allowed;
        }

        return await CheckRestaurantAsync(restaurantId, request);
    }

    private async Task<ResourceAccessResult> CheckRestaurantAsync(Guid? restaurantId, ResourceAccessRequest request)
    {
        var restaurant = await _repository.GetRestaurantAsync(restaurantId);
        if (restaurant == null)
        {
            return ResourceAccessResult.NotFound;
        }

        if (request.UserId != Guid.Empty && restaurant.OwnerId == request.UserId)
        {
            return ResourceAccessResult.Allowed;
        }

        if (!request.IsRead)
        {
            return ResourceAccessResult.Forbidden;
        }

        if (!restaurant.IsActive)
        {
            return ResourceAccessResult.NotFound;
        }

        if (restaurant.ApprovalStatus != RestaurantApprovalStatus.Approved)
        {
            return ResourceAccessResult.NotFound;
        }

        return ResourceAccessResult.Allowed;
    }
}
