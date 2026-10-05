using System.Security.Claims;
using FoodOrdering.Api.Models.Entities;
using FoodOrdering.Api.Models.Requests;

namespace FoodOrdering.Api.Services.Interfaces;

public interface IPaymentsService
{
    Task<ServiceResult> CollectAsync(Guid orderId, PaymentActionRequest request, ClaimsPrincipal actor);
    Task<ServiceResult> SubmitToOwnerAsync(Guid orderId, PaymentActionRequest request, ClaimsPrincipal actor);
}
