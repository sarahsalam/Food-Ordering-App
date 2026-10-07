using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public record ProductVariantQuery(Guid Id):IRequest<ProductVariantResponse?>;
