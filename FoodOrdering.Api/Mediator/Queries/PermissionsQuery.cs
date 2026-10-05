using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public record PermissionsQuery(Guid UserId) : IRequest<PermissionsResponse>;
