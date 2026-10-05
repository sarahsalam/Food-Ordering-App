using FoodOrdering.Api.Services;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public class AssignRolesToUserCommandHandler : IRequestHandler<AssignRolesToUserCommand, ServiceResult>
{
    private readonly RbacService _service;

    public AssignRolesToUserCommandHandler(RbacService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(AssignRolesToUserCommand request, CancellationToken cancellationToken)
    {
        return _service.AssignRolesAsync(request.UserId, request.Roles);
    }
}
