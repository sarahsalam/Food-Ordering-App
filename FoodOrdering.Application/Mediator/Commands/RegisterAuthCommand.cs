using FoodOrdering.Application.DTOs.Requests;
using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record RegisterAuthCommand(RegisterRequest Request) : IRequest<(bool Success, string? Error)>;
