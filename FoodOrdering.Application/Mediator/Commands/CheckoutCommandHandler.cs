using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public sealed class CheckoutCommandHandler(ICheckoutService service)
    : IRequestHandler<CheckoutCommand, (bool Success, string? Error, OrderResponse? Data)>
{
    public Task<(bool Success, string? Error, OrderResponse? Data)> Handle(
        CheckoutCommand request, CancellationToken cancellationToken) =>
        service.CheckoutAsync(request.UserId, request.Request, cancellationToken);
}
