using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public sealed class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserResponse>>
{
    private readonly IUserService _service;

    public GetUsersQueryHandler(IUserService service)
    {
        _service = service;
    }

    public Task<List<UserResponse>> Handle(
        GetUsersQuery request,
        CancellationToken cancellationToken) => _service.GetAllAsync();
}
