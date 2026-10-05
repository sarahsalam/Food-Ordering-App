using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public sealed class UpdateProductCommandHandler
    : IRequestHandler<UpdateProductCommand, (bool Success, string? Error,
        ProductResponse? Data)>
{
    private readonly IProductService _service;

    public UpdateProductCommandHandler(IProductService service)
    {
        _service = service;
    }

    public Task<(bool Success, string? Error, ProductResponse? Data)> Handle(
        UpdateProductCommand request, CancellationToken cancellationToken) =>
        _service.UpdateAsync(request.Id, request.Request);
}