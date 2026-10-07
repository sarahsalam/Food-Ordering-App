using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

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
