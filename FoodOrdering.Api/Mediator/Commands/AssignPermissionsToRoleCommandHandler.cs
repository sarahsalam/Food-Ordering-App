using FoodOrdering.Api.Services;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public class AssignPermissionsToRoleCommandHandler : IRequestHandler<AssignPermissionsToRoleCommand, ServiceResult>
{
    private readonly RbacService _service;

    public AssignPermissionsToRoleCommandHandler(RbacService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(AssignPermissionsToRoleCommand request, CancellationToken cancellationToken)
    {
        return _service.AssignPermissionsAsync(request.RoleId, request.Permissions);
    }
}
