using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public class RefreshAuthCommandHandler : IRequestHandler<RefreshAuthCommand, (bool Success, string? Error, string? AccessToken, string? RefreshToken)>
{
    private readonly IAuthService _service;

    public RefreshAuthCommandHandler(IAuthService service)
    {
        _service = service;
    }

    public Task<(bool Success, string? Error, string? AccessToken, string? RefreshToken)> Handle(RefreshAuthCommand request, CancellationToken cancellationToken)
    {
        return _service.RefreshAsync(request.Request.RefreshToken);
    }
}
