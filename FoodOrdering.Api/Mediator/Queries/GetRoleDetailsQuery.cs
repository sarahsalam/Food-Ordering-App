using FoodOrdering.Api.Services;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public record GetRoleDetailsQuery(Guid RoleId) : IRequest<ServiceResult>;
