using FoodOrdering.Api.Services;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public record GetUserAccessQuery(Guid UserId) : IRequest<ServiceResult>;
