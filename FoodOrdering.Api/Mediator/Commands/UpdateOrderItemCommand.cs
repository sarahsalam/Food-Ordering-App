using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public record UpdateOrderItemCommand(Guid Id, UpdateOrderItemRequest Request)
    : IRequest<(bool Success, string? Error, OrderItemResponse? Data)>;