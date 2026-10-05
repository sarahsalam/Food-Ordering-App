using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public record CreateCartItemCommand(Guid CartId, CreateCartItemRequest Request)
    : IRequest<(bool Success, string? Error, CartItemResponse? Data)>;