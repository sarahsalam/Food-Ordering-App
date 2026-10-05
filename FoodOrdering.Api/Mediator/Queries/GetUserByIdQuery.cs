using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public sealed record GetUserByIdQuery(Guid Id) : IRequest<UserResponse?>;
