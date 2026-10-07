using FoodOrdering.Application.DTOs.Requests;

namespace FoodOrdering.Application.Services.Interfaces;

public interface IPaymentsService
{
    Task<ServiceResult> CollectAsync(Guid orderId, PaymentActionRequest request, ClaimsPrincipal actor);
    Task<ServiceResult> SubmitToOwnerAsync(Guid orderId, PaymentActionRequest request, ClaimsPrincipal actor);
}
