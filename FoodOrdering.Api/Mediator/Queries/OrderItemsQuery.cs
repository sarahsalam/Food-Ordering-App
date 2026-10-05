using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public record OrderItemsQuery(Guid OrderId):IRequest<(bool Exists,List<OrderItemResponse> Data)>;

