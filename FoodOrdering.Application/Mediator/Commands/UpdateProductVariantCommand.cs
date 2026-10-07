using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record UpdateProductVariantCommand(Guid Id, UpdateProductVariantRequest Request)
    : IRequest<(bool Success, string? Error, ProductVariantResponse? Data)>;
