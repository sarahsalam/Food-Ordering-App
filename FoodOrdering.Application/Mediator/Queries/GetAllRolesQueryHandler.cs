using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, ServiceResult>
{
    private readonly IRbacService _service;

    public GetAllRolesQueryHandler(IRbacService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        return _service.ListRolesAsync();
    }
}
