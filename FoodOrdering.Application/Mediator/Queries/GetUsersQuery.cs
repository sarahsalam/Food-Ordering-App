using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public sealed record GetUsersQuery : IRequest<List<UserResponse>>;
