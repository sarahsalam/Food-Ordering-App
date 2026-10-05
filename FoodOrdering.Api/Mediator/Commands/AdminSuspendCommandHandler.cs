using FoodOrdering.Api.Services;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public class AdminSuspendCommandHandler : IRequestHandler<AdminSuspendCommand, ServiceResult>
{
    private readonly IAdminService _service;

    public AdminSuspendCommandHandler(IAdminService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(AdminSuspendCommand request, CancellationToken cancellationToken)
    {
        return _service.SuspendAsync(request.Id, request.Request, request.Actor);
    }
}
