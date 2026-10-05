using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public sealed record UpdateBranchCommand(Guid Id, UpdateBranchRequest Request)
    : IRequest<(bool Success, string? Error, BranchResponse? Data)>;
