using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record CreateOrderItemCommand(Guid OrderId, CreateOrderItemRequest Request)
    : IRequest<(bool Success, string? Error, OrderItemResponse? Data)>;
