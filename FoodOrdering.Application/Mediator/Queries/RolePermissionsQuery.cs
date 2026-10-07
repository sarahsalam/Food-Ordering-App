using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public record RolePermissionsQuery(Guid RoleId) : IRequest<ServiceResult>;
