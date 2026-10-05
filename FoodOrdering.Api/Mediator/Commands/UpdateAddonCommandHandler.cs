using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public sealed class UpdateAddonCommandHandler
    : IRequestHandler<UpdateAddonCommand, (bool Success, string? Error, AddonResponse?
        Data)>
{
    private readonly IAddonService _service;

    public UpdateAddonCommandHandler(IAddonService service)
    {
        _service = service;
    }

    public Task<(bool Success, string? Error, AddonResponse? Data)> Handle(
        UpdateAddonCommand request, CancellationToken cancellationToken) =>
        _service.UpdateAsync(request.Id, request.Request);
}