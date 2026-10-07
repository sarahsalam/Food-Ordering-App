using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record CreateCartItemCommand(Guid CartId, CreateCartItemRequest Request)
    : IRequest<(bool Success, string? Error, CartItemResponse? Data)>;
