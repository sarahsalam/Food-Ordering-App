using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public record UpdateCartItemCommand(Guid Id, UpdateCartItemRequest Request)
    : IRequest<(bool Success, string? Error, CartItemResponse? Data)>;