using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public class AssignRolePermissionCommandHandler : IRequestHandler<AssignRolePermissionCommand, ServiceResult>
{
    private readonly IRolePermissionService _service;

    public AssignRolePermissionCommandHandler(IRolePermissionService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(AssignRolePermissionCommand request, CancellationToken cancellationToken)
    {
        return _service.AssignAsync(request.RoleId, request.PermissionId);
    }
}
