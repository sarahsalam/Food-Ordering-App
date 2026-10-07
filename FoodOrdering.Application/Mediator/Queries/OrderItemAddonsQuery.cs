using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public record OrderItemAddonsQuery(Guid OrderItemId) : IRequest<(bool Exists, List<OrderItemAddonResponse> Data)>;
