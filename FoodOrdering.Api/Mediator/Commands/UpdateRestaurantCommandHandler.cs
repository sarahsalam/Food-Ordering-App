using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public sealed class UpdateRestaurantCommandHandler
    : IRequestHandler<UpdateRestaurantCommand, (bool Success, string? Error, RestaurantResponse? Data)>
{
    private readonly IRestaurantService _service;

    public UpdateRestaurantCommandHandler(IRestaurantService service)
    {
        _service = service;
    }

    public Task<(bool Success, string? Error, RestaurantResponse? Data)> Handle(
        UpdateRestaurantCommand request,
        CancellationToken cancellationToken) =>
        _service.UpdateAsync(request.Id, request.Request);
}
