using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public sealed class UpdateCategoryCommandHandler
    : IRequestHandler<UpdateCategoryCommand, (bool Success, string? Error, CategoryResponse? Data)>
{
    private readonly ICategoryService _service;

    public UpdateCategoryCommandHandler(ICategoryService service)
    {
        _service = service;
    }

    public Task<(bool Success, string? Error, CategoryResponse? Data)> Handle(
        UpdateCategoryCommand request,
        CancellationToken cancellationToken) =>
        _service.UpdateAsync(request.Id, request.Request);
}
