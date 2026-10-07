using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public record CustomerCatalogGetQuery(ClaimsPrincipal Actor) : IRequest<ServiceResult>;
