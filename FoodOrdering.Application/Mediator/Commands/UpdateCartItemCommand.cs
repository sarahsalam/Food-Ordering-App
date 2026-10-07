using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record UpdateCartItemCommand(Guid Id, UpdateCartItemRequest Request)
    : IRequest<(bool Success, string? Error, CartItemResponse? Data)>;
