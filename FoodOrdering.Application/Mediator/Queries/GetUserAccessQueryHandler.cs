using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public class GetUserAccessQueryHandler : IRequestHandler<GetUserAccessQuery, ServiceResult>
{
    private readonly IRbacService _service;

    public GetUserAccessQueryHandler(IRbacService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(GetUserAccessQuery request, CancellationToken cancellationToken)
    {
        return _service.UserRolesAsync(request.UserId);
    }
}
