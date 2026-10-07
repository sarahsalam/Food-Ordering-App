using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

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
