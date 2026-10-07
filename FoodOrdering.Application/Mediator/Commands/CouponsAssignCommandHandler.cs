using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public class CouponsAssignCommandHandler : IRequestHandler<CouponsAssignCommand, ServiceResult>
{
    private readonly ICouponsService _service;

    public CouponsAssignCommandHandler(ICouponsService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(CouponsAssignCommand request, CancellationToken cancellationToken)
    {
        return _service.AssignAsync(request.CouponId, request.UserId, request.Actor);
    }
}
