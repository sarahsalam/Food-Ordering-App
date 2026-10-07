using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public sealed class DeleteCartCommandHandler
    : IRequestHandler<DeleteCartCommand, (bool Success, string? Error)>
{
    private readonly ICartService _service;

    public DeleteCartCommandHandler(ICartService service)
    {
        _service = service;
    }

    public Task<(bool Success, string? Error)> Handle(DeleteCartCommand request, CancellationToken cancellationToken) =>
        _service.DeleteAsync(request.Id);
}
