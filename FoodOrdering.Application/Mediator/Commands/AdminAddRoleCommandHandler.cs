using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

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
