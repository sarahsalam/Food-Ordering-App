using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public sealed record GetBranchByIdQuery(Guid Id) : IRequest<BranchResponse?>;
