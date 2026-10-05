using FoodOrdering.Api.Services;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public class GetRoleDetailsQueryHandler : IRequestHandler<GetRoleDetailsQuery, ServiceResult>
{
    private readonly RbacService _service;

    public GetRoleDetailsQueryHandler(RbacService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(GetRoleDetailsQuery request, CancellationToken cancellationToken)
    {
        return _service.RoleAsync(request.RoleId);
    }
}
