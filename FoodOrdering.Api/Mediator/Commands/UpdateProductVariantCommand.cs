using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public record UpdateProductVariantCommand(Guid Id, UpdateProductVariantRequest Request)
    : IRequest<(bool Success, string? Error, ProductVariantResponse? Data)>;