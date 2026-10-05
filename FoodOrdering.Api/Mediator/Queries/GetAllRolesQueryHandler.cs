using FoodOrdering.Api.Services;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, ServiceResult>
{
    private readonly RbacService _service;

    public GetAllRolesQueryHandler(RbacService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        return _service.ListRolesAsync();
    }
}
