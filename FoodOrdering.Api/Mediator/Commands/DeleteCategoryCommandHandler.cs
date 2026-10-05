using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public sealed class DeleteCategoryCommandHandler
    : IRequestHandler<DeleteCategoryCommand, (bool Success, string? Error)>
{
    private readonly ICategoryService _service;

    public DeleteCategoryCommandHandler(ICategoryService service)
    {
        _service = service;
    }

    public Task<(bool Success, string? Error)> Handle(
        DeleteCategoryCommand request,
        CancellationToken cancellationToken) =>
        _service.DeleteAsync(request.Id);
}
