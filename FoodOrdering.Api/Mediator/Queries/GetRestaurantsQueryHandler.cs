using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public sealed class GetRestaurantsQueryHandler
    : IRequestHandler<GetRestaurantsQuery, List<RestaurantResponse>>
{
    private readonly IRestaurantService _service;

    public GetRestaurantsQueryHandler(IRestaurantService service)
    {
        _service = service;
    }

    public Task<List<RestaurantResponse>> Handle(
        GetRestaurantsQuery request,
        CancellationToken cancellationToken) => _service.GetAllAsync(request.OwnerId, request.Query);
}
