using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public sealed class AddProductAddonCommandHandler
    : IRequestHandler<AddProductAddonCommand, (bool Success, string? Error,
        ProductAddonResponse? Data)>
{
    private readonly IProductAddonService _service;

    public AddProductAddonCommandHandler(IProductAddonService service)
    {
        _service = service;
    }

    public Task<(bool Success, string? Error, ProductAddonResponse? Data)> Handle(
        AddProductAddonCommand request, CancellationToken cancellationToken) =>
        _service.AddAsync(request.ProductId, request.AddonId);
}