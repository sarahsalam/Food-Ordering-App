using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public class LoginAuthCommandHandler : IRequestHandler<LoginAuthCommand, (bool Success, string? Error, string? AccessToken, string? RefreshToken, UserResponse? User)>
{
    private readonly IAuthService _service;

    public LoginAuthCommandHandler(IAuthService service)
    {
        _service = service;
    }

    public Task<(bool Success, string? Error, string? AccessToken, string? RefreshToken, UserResponse? User)> Handle(LoginAuthCommand request, CancellationToken cancellationToken)
    {
        return _service.LoginAsync(request.Request);
    }
}
