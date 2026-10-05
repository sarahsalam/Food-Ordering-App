using FoodOrdering.Api.Services;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public class RemoveRolePermissionCommandHandler : IRequestHandler<RemoveRolePermissionCommand, ServiceResult>
{
    private readonly IRolePermissionService _service;

    public RemoveRolePermissionCommandHandler(IRolePermissionService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(RemoveRolePermissionCommand request, CancellationToken cancellationToken)
    {
        return _service.RemoveAsync(request.RoleId, request.PermissionId);
    }
}
