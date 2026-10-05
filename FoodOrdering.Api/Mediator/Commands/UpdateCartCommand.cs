using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public record UpdateCartCommand(Guid Id, UpdateCartRequest Request)
    : IRequest<(bool Success, string? Error, CartResponse? Data)>;