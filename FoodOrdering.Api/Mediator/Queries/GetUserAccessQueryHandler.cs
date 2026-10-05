using FoodOrdering.Api.Services;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public class GetUserAccessQueryHandler : IRequestHandler<GetUserAccessQuery, ServiceResult>
{
    private readonly RbacService _service;

    public GetUserAccessQueryHandler(RbacService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(GetUserAccessQuery request, CancellationToken cancellationToken)
    {
        return _service.UserRolesAsync(request.UserId);
    }
}
