using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public class RevokePermissionFromRoleCommandHandler : IRequestHandler<RevokePermissionFromRoleCommand, ServiceResult>
{
    private readonly IRbacService _service;

    public RevokePermissionFromRoleCommandHandler(IRbacService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(RevokePermissionFromRoleCommand request, CancellationToken cancellationToken)
    {
        return _service.RevokePermissionAsync(request.RoleId, request.PermissionId);
    }
}
