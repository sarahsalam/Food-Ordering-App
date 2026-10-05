using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public record ProductQuery(Guid Id):IRequest<ProductResponse?>;
