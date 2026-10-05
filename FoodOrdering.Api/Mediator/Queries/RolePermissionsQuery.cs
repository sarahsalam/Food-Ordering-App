using FoodOrdering.Api.Services;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public record RolePermissionsQuery(Guid RoleId) : IRequest<ServiceResult>;
