using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public sealed class CreateAddonCommandHandler
    : IRequestHandler<CreateAddonCommand, (bool Success, string? Error, AddonResponse?
        Data)>
{
    private readonly IAddonService _service;

    public CreateAddonCommandHandler(IAddonService service)
    {
        _service = service;
    }

    public Task<(bool Success, string? Error, AddonResponse? Data)> Handle(
        CreateAddonCommand request,
        CancellationToken cancellationToken) =>
        _service.CreateAsync(request.RestaurantId, request.Request);
}