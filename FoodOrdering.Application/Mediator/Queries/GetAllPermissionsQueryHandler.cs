using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public class GetAllPermissionsQueryHandler : IRequestHandler<GetAllPermissionsQuery, ServiceResult>
{
    private readonly IRbacService _service;

    public GetAllPermissionsQueryHandler(IRbacService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
    {
        return _service.ListPermissionsAsync();
    }
}
