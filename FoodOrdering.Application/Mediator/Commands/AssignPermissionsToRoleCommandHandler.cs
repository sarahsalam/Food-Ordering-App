using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public class AssignPermissionsToRoleCommandHandler : IRequestHandler<AssignPermissionsToRoleCommand, ServiceResult>
{
    private readonly IRbacService _service;

    public AssignPermissionsToRoleCommandHandler(IRbacService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(AssignPermissionsToRoleCommand request, CancellationToken cancellationToken)
    {
        return _service.AssignPermissionsAsync(request.RoleId, request.Permissions);
    }
}
