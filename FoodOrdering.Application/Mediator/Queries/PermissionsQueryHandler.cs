using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public class PermissionsQueryHandler : IRequestHandler<PermissionsQuery, PermissionsResponse>
{
    private readonly UserAccessService _service;

    public PermissionsQueryHandler(UserAccessService service)
    {
        _service = service;
    }

    public async Task<PermissionsResponse> Handle(PermissionsQuery request, CancellationToken cancellationToken)
    {
        await _service.LoadAsync(request.UserId);
        return new PermissionsResponse(_service.Roles, _service.Permissions);
    }
}
