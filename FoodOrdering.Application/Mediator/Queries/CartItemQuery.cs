using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public record CartItemQuery(Guid Id):IRequest<CartItemResponse?>;
