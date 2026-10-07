using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public record DeliveriesGetQuery(ClaimsPrincipal Actor) : IRequest<ServiceResult>;
