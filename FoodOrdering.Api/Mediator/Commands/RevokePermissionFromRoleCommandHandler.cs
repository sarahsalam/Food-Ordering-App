using FoodOrdering.Api.Services;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public class RevokePermissionFromRoleCommandHandler : IRequestHandler<RevokePermissionFromRoleCommand, ServiceResult>
{
    private readonly RbacService _service;

    public RevokePermissionFromRoleCommandHandler(RbacService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(RevokePermissionFromRoleCommand request, CancellationToken cancellationToken)
    {
        return _service.RevokePermissionAsync(request.RoleId, request.PermissionId);
    }
}
