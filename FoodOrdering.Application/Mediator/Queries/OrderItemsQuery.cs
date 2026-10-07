using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public record OrderItemsQuery(Guid OrderId):IRequest<(bool Exists, List<OrderItemResponse> Data)>;

