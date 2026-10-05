using FoodOrdering.Api.Services;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public class AdminAssignStaffCommandHandler : IRequestHandler<AdminAssignStaffCommand, ServiceResult>
{
    private readonly IAdminService _service;

    public AdminAssignStaffCommandHandler(IAdminService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(AdminAssignStaffCommand request, CancellationToken cancellationToken)
    {
        return _service.AssignStaffAsync(request.RestaurantId, request.UserId, request.Actor);
    }
}
