using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public class RegisterAuthCommandHandler : IRequestHandler<RegisterAuthCommand, (bool Success, string? Error)>
{
    private readonly IAuthService _service;

    public RegisterAuthCommandHandler(IAuthService service)
    {
        _service = service;
    }

    public Task<(bool Success, string? Error)> Handle(RegisterAuthCommand request, CancellationToken cancellationToken)
    {
        return _service.RegisterAsync(request.Request);
    }
}
