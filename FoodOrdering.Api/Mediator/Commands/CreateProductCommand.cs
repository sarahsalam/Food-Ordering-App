using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public record CreateProductCommand(Guid CategoryId, CreateProductRequest Request)
    : IRequest<(bool Success, string? Error, ProductResponse? Data)>;