using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public class AdminRestaurantsQueryHandler : IRequestHandler<AdminRestaurantsQuery, ServiceResult>
{
    private readonly IAdminService _service;

    public AdminRestaurantsQueryHandler(IAdminService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(AdminRestaurantsQuery request, CancellationToken cancellationToken)
    {
        return _service.RestaurantsAsync(request.ApprovalStatus, request.Actor);
    }
}
