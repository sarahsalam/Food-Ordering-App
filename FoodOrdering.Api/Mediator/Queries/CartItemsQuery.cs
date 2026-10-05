using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public record CartItemsQuery(Guid CartId) : IRequest<(bool Exists, List<CartItemResponse> Data)>;