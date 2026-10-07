using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public sealed class CreateCategoryCommandHandler
    : IRequestHandler<CreateCategoryCommand, (bool Success, string? Error, CategoryResponse? Data)>
{
    private readonly ICategoryService _service;

    public CreateCategoryCommandHandler(ICategoryService service)
    {
        _service = service;
    }

    public Task<(bool Success, string? Error, CategoryResponse? Data)> Handle(
        CreateCategoryCommand request,
        CancellationToken cancellationToken) =>
        _service.CreateAsync(request.RestaurantId, request.Request);
}
