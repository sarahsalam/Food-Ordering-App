using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public record GetRoleDetailsQuery(Guid RoleId) : IRequest<ServiceResult>;
