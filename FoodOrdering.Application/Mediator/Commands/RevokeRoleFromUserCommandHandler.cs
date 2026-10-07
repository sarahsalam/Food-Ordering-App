using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public class RevokeRoleFromUserCommandHandler : IRequestHandler<RevokeRoleFromUserCommand, ServiceResult>
{
    private readonly IRbacService _service;

    public RevokeRoleFromUserCommandHandler(IRbacService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(RevokeRoleFromUserCommand request, CancellationToken cancellationToken)
    {
        return _service.RevokeRoleAsync(request.UserId, request.RoleId);
    }
}
