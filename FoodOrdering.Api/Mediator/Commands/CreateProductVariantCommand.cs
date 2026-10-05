using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public record CreateProductVariantCommand(Guid ProductId, CreateProductVariantRequest Request)
    : IRequest<(bool Success, string? Error, ProductVariantResponse? Data)>;