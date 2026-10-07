using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public record GetUserAccessQuery(Guid UserId) : IRequest<ServiceResult>;
