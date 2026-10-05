using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public record ProductVariantQuery(Guid Id):IRequest<ProductVariantResponse?>;
