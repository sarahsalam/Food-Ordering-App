using FoodOrdering.Api.Services;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, ServiceResult>
{
    private readonly RbacService _service;

    public DeleteRoleCommandHandler(RbacService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        return _service.DeleteRoleAsync(request.RoleId);
    }
}
