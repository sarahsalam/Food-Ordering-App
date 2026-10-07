using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public class AdminRejectCommandHandler : IRequestHandler<AdminRejectCommand, ServiceResult>
{
    private readonly IAdminService _service;

    public AdminRejectCommandHandler(IAdminService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(AdminRejectCommand request, CancellationToken cancellationToken)
    {
        return _service.RejectAsync(request.Id, request.Request, request.Actor);
    }
}
