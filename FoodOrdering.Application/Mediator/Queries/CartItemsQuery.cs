using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public record CartItemsQuery(Guid CartId) : IRequest<(bool Exists, List<CartItemResponse> Data)>;
