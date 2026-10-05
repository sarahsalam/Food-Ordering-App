using FoodOrdering.Api.Services;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public class RestaurantOperationsOrdersQueryHandler : IRequestHandler<RestaurantOperationsOrdersQuery, ServiceResult>
{
    private readonly IRestaurantOperationsService _service;

    public RestaurantOperationsOrdersQueryHandler(IRestaurantOperationsService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(RestaurantOperationsOrdersQuery request, CancellationToken cancellationToken)
    {
        return _service.OrdersAsync(request.RestaurantId, request.Actor);
    }
}
