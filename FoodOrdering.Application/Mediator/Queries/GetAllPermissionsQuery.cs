using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public record GetAllPermissionsQuery() : IRequest<ServiceResult>;
