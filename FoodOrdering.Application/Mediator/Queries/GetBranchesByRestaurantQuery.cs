using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public sealed record GetBranchesByRestaurantQuery(
    Guid RestaurantId,
    BranchQueryRequest Query)
    : IRequest<(bool Exists, List<BranchResponse> Data)>;
