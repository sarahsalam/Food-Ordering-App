using FoodOrdering.Api.Services;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public class AdminApproveCommandHandler : IRequestHandler<AdminApproveCommand, ServiceResult>
{
    private readonly IAdminService _service;

    public AdminApproveCommandHandler(IAdminService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(AdminApproveCommand request, CancellationToken cancellationToken)
    {
        return _service.ApproveAsync(request.Id, request.Request, request.Actor);
    }
}
