using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public record MyCartGetQuery(ClaimsPrincipal Actor) : IRequest<ServiceResult>;
