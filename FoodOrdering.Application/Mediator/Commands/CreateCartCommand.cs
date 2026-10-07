using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record CreateCartCommand(CreateCartRequest Request)
    : IRequest<(bool Success, string? Error, CartResponse? Data)>;
