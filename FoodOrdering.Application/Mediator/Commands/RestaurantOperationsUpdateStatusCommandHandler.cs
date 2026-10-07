using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public class RestaurantOperationsUpdateStatusCommandHandler : IRequestHandler<RestaurantOperationsUpdateStatusCommand, ServiceResult>
{
    private readonly IRestaurantOperationsService _service;

    public RestaurantOperationsUpdateStatusCommandHandler(IRestaurantOperationsService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(RestaurantOperationsUpdateStatusCommand request, CancellationToken cancellationToken)
    {
        return _service.UpdateStatusAsync(request.Id, request.Request, request.Actor);
    }
}
