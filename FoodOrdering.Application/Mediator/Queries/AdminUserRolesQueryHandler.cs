using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public class AdminUserRolesQueryHandler : IRequestHandler<AdminUserRolesQuery, ServiceResult>
{
    private readonly IAdminService _service;

    public AdminUserRolesQueryHandler(IAdminService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(AdminUserRolesQuery request, CancellationToken cancellationToken)
    {
        return _service.UserRolesAsync(request.UserId, request.Actor);
    }
}
