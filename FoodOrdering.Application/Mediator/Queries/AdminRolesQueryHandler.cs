using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public class AdminRolesQueryHandler : IRequestHandler<AdminRolesQuery, ServiceResult>
{
    private readonly IAdminService _service;

    public AdminRolesQueryHandler(IAdminService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(AdminRolesQuery request, CancellationToken cancellationToken)
    {
        return _service.RolesAsync(request.Actor);
    }
}
