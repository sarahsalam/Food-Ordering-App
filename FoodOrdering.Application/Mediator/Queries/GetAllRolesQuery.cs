using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public record GetAllRolesQuery() : IRequest<ServiceResult>;
