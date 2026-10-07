using FoodOrdering.Application.DTOs.Requests;
using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record PaymentsSubmitToOwnerCommand(Guid OrderId, PaymentActionRequest Request, ClaimsPrincipal Actor) : IRequest<ServiceResult>;
