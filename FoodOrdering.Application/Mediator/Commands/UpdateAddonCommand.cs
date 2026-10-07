using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record UpdateAddonCommand(Guid Id, UpdateAddonRequest Request)
    : IRequest<(bool Success, string? Error, AddonResponse? Data)>;
