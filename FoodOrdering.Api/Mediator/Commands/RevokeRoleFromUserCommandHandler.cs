using FoodOrdering.Api.Services;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public class RevokeRoleFromUserCommandHandler : IRequestHandler<RevokeRoleFromUserCommand, ServiceResult>
{
    private readonly RbacService _service;

    public RevokeRoleFromUserCommandHandler(RbacService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(RevokeRoleFromUserCommand request, CancellationToken cancellationToken)
    {
        return _service.RevokeRoleAsync(request.UserId, request.RoleId);
    }
}
