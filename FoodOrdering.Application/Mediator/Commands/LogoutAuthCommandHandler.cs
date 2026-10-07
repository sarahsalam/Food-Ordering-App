using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public class LogoutAuthCommandHandler : IRequestHandler<LogoutAuthCommand>
{
    private readonly IAuthService _service;

    public LogoutAuthCommandHandler(IAuthService service)
    {
        _service = service;
    }

    public Task Handle(LogoutAuthCommand request, CancellationToken cancellationToken)
    {
        return _service.LogoutAsync(request.Request.RefreshToken);
    }
}
