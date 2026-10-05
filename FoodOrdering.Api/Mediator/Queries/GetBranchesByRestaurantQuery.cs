using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public sealed record GetBranchesByRestaurantQuery(
    Guid RestaurantId,
    BranchQueryRequest Query)
    : IRequest<(bool Exists, List<BranchResponse> Data)>;
