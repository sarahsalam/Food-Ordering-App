using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public record CheckoutCommand(Guid UserId, CheckoutRequest Request)
    : IRequest<(bool Success, string? Error, OrderResponse? Data)>;
