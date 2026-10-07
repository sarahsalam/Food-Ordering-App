using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public record PermissionsQuery(Guid UserId) : IRequest<PermissionsResponse>;
