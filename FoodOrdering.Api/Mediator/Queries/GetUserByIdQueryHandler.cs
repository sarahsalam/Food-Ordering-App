using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserResponse?>
{
    private readonly IUserService _service;

    public GetUserByIdQueryHandler(IUserService service)
    {
        _service = service;
    }

    public Task<UserResponse?> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken) => _service.GetByIdAsync(request.Id);
}
