using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public sealed record CreateBranchCommand(
    Guid RestaurantId,
    Guid OwnerId,
    CreateBranchRequest Request)
    : IRequest<(bool Success, string? Error, BranchResponse? Data)>;
