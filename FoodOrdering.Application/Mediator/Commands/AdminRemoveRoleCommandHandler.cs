using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public class AdminRemoveRoleCommandHandler : IRequestHandler<AdminRemoveRoleCommand, ServiceResult>
{
    private readonly IAdminService _service;

    public AdminRemoveRoleCommandHandler(IAdminService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(AdminRemoveRoleCommand request, CancellationToken cancellationToken)
    {
        return _service.RemoveRoleAsync(request.UserId, request.RoleId, request.Actor);
    }
}
