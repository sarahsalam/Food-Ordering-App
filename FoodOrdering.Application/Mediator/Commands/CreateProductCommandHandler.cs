using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public sealed class CreateProductCommandHandler
    : IRequestHandler<CreateProductCommand, (bool Success, string? Error, ProductResponse? Data)>
{
    private readonly IProductService _service;

    public CreateProductCommandHandler(IProductService service)
    {
        _service = service;
    }

    public Task<(bool Success, string? Error, ProductResponse? Data)> Handle(
        CreateProductCommand request, CancellationToken cancellationToken) =>
        _service.CreateAsync(request.CategoryId, request.Request);
}
