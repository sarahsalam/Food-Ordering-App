using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

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
