using FoodOrdering.Api.Services;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public class AdminAddRoleCommandHandler : IRequestHandler<AdminAddRoleCommand, ServiceResult>
{
    private readonly IAdminService _service;

    public AdminAddRoleCommandHandler(IAdminService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(AdminAddRoleCommand request, CancellationToken cancellationToken)
    {
        return _service.AddRoleAsync(request.UserId, request.Request, request.Actor);
    }
}
