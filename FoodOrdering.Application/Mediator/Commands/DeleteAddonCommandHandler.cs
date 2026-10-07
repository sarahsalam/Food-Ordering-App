using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public sealed class DeleteAddonCommandHandler
    : IRequestHandler<DeleteAddonCommand, (bool Success, string? Error)>
{
    private readonly IAddonService _service;

    public DeleteAddonCommandHandler(IAddonService service)
    {
        _service = service;
    }

    public Task<(bool Success, string? Error)>
        Handle(DeleteAddonCommand request, CancellationToken cancellationToken) => _service.DeleteAsync(request.Id);
}
