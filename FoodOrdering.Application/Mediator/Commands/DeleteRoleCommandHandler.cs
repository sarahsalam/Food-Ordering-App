using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, ServiceResult>
{
    private readonly IRbacService _service;

    public DeleteRoleCommandHandler(IRbacService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        return _service.DeleteRoleAsync(request.RoleId);
    }
}
