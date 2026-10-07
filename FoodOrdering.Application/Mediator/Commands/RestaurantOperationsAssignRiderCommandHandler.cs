using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public class RestaurantOperationsAssignRiderCommandHandler : IRequestHandler<RestaurantOperationsAssignRiderCommand, ServiceResult>
{
    private readonly IRestaurantOperationsService _service;

    public RestaurantOperationsAssignRiderCommandHandler(IRestaurantOperationsService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(RestaurantOperationsAssignRiderCommand request, CancellationToken cancellationToken)
    {
        return _service.AssignRiderAsync(request.Id, request.Request, request.Actor);
    }
}
