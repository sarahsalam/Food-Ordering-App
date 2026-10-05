using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public sealed class CreateRestaurantCommandHandler
    : IRequestHandler<CreateRestaurantCommand, (bool Success, string? Error, RestaurantResponse? Data)>
{
    private readonly IRestaurantService _service;

    public CreateRestaurantCommandHandler(IRestaurantService service)
    {
        _service = service;
    }

    public Task<(bool Success, string? Error, RestaurantResponse? Data)> Handle(
        CreateRestaurantCommand request,
        CancellationToken cancellationToken) =>
        _service.CreateAsync(request.OwnerId, request.Request);
}
