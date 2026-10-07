using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record UpdateProductCommand(Guid Id, UpdateProductRequest Request)
    : IRequest<(bool Success, string? Error, ProductResponse? Data)>;
