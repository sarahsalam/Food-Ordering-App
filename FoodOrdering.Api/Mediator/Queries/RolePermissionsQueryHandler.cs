using FoodOrdering.Api.Services;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public class RolePermissionsQueryHandler : IRequestHandler<RolePermissionsQuery, ServiceResult>
{
    private readonly IRolePermissionService _service;

    public RolePermissionsQueryHandler(IRolePermissionService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(RolePermissionsQuery request, CancellationToken cancellationToken)
    {
        return _service.GetByRoleAsync(request.RoleId);
    }
}
