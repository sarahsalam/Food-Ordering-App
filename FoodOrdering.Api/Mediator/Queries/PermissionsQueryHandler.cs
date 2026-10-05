using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Security;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

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
