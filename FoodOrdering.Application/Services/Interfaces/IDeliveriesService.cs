using FoodOrdering.Application.DTOs.Requests;

namespace FoodOrdering.Application.Services.Interfaces;

public interface IDeliveriesService
{
    Task<ServiceResult> GetAsync(ClaimsPrincipal actor);
    Task<ServiceResult> UpdateStatusAsync(Guid id, ChangeOrderStatusRequest request, ClaimsPrincipal actor);
}
