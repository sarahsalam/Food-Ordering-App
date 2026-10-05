using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public record CreateAddonCommand(Guid RestaurantId, CreateAddonRequest Request)
    : IRequest<(bool Success, string? Error, AddonResponse? Data)>;