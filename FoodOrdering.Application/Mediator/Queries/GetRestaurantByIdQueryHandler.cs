using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public sealed class GetRestaurantByIdQueryHandler
    : IRequestHandler<GetRestaurantByIdQuery, RestaurantResponse?>
{
    private readonly IRestaurantService _service;

    public GetRestaurantByIdQueryHandler(IRestaurantService service)
    {
        _service = service;
    }

    public Task<RestaurantResponse?> Handle(
        GetRestaurantByIdQuery request,
        CancellationToken cancellationToken) => _service.GetByIdAsync(request.OwnerId, request.Id);
}
