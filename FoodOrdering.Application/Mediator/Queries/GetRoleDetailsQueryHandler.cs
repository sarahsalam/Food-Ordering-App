using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public class GetRoleDetailsQueryHandler : IRequestHandler<GetRoleDetailsQuery, ServiceResult>
{
    private readonly IRbacService _service;

    public GetRoleDetailsQueryHandler(IRbacService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(GetRoleDetailsQuery request, CancellationToken cancellationToken)
    {
        return _service.RoleAsync(request.RoleId);
    }
}
