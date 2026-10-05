using FoodOrdering.Api.Services;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public class GetAllPermissionsQueryHandler : IRequestHandler<GetAllPermissionsQuery, ServiceResult>
{
    private readonly RbacService _service;

    public GetAllPermissionsQueryHandler(RbacService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
    {
        return _service.ListPermissionsAsync();
    }
}
