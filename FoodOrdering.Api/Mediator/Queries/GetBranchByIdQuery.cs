using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public sealed record GetBranchByIdQuery(Guid Id) : IRequest<BranchResponse?>;
