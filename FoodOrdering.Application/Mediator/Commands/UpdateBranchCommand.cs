using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public sealed record UpdateBranchCommand(Guid Id, UpdateBranchRequest Request)
    : IRequest<(bool Success, string? Error, BranchResponse? Data)>;
