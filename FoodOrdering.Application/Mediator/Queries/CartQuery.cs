using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public record CartQuery(Guid Id) : IRequest<CartResponse?>;
