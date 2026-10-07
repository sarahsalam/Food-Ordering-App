using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public class AssignRolesToUserCommandHandler : IRequestHandler<AssignRolesToUserCommand, ServiceResult>
{
    private readonly IRbacService _service;

    public AssignRolesToUserCommandHandler(IRbacService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(AssignRolesToUserCommand request, CancellationToken cancellationToken)
    {
        return _service.AssignRolesAsync(request.UserId, request.Roles);
    }
}
