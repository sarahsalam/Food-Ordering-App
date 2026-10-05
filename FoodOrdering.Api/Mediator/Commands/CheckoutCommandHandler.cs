using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public sealed class CheckoutCommandHandler(ICheckoutService service)
    : IRequestHandler<CheckoutCommand, (bool Success, string? Error, OrderResponse? Data)>
{
    public Task<(bool Success, string? Error, OrderResponse? Data)> Handle(
        CheckoutCommand request, CancellationToken cancellationToken) =>
        service.CheckoutAsync(request.UserId, request.Request, cancellationToken);
}
