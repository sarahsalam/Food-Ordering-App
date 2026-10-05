using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Repositories.Interfaces;
using FoodOrdering.Api.Services.Interfaces;

namespace FoodOrdering.Api.Services;

public class ResourceAccessService : IResourceAccessService
{
    private readonly IResourceAccessRepository _repository;
    private readonly IRestaurantAccessService _restaurants;

    public ResourceAccessService(IResourceAccessRepository repository, IRestaurantAccessService restaurants)
    {
        _repository = repository;
        _restaurants = restaurants;
    }

    public async Task<ResourceAccessResult> CheckAsync(ResourceAccessRequest request)
    {
        bool allowed;
        switch (request.Controller)
        {
            case "Carts":
                if (request.Id == Guid.Empty)
                {
                    return ResourceAccessResult.Allowed;
                }
                allowed = await _repository.OwnsCartAsync(request.Id, request.UserId);
                break;

            case "CartItems":
                if (request.CartId != Guid.Empty)
                {
                    allowed = await _repository.OwnsCartAsync(request.CartId, request.UserId);
                }
                else
                {
                    allowed = await _repository.OwnsCartItemAsync(request.Id, request.UserId);
                }
                break;

            case "Orders":
                if (request.Id == Guid.Empty)
                {
                    return ResourceAccessResult.Allowed;
                }
                allowed = await _repository.OwnsOrderAsync(request.Id, request.UserId);
                break;

            case "OrderItems":
                if (request.OrderId != Guid.Empty)
                {
                    allowed = await _repository.OwnsOrderAsync(request.OrderId, request.UserId);
                }
                else
                {
                    allowed = await _repository.OwnsOrderItemAsync(request.Id, request.UserId);
                }
                break;

            case "OrderItemAddons":
                if (request.OrderItemId != Guid.Empty)
                {
                    allowed = await _repository.OwnsOrderItemAsync(request.OrderItemId, request.UserId);
                }
                else
                {
                    allowed = await _repository.OwnsOrderItemAddonAsync(request.Id, request.UserId);
                }
                break;

            default:
                return await _restaurants.CheckAsync(request);
        }

        if (!allowed)
        {
            return ResourceAccessResult.NotFound;
        }
        return ResourceAccessResult.Allowed;
    }
}
