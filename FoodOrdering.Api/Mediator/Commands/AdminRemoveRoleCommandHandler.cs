using FoodOrdering.Api.Services;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

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
