using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public sealed record CreateBranchCommand(
    Guid RestaurantId,
    Guid OwnerId,
    CreateBranchRequest Request)
    : IRequest<(bool Success, string? Error, BranchResponse? Data)>;
