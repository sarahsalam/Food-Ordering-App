using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public record ProductQuery(Guid Id):IRequest<ProductResponse?>;
