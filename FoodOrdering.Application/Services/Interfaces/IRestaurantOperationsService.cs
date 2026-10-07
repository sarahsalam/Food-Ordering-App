using FoodOrdering.Application.DTOs.Requests;

namespace FoodOrdering.Application.Services.Interfaces;

public interface IRestaurantOperationsService
{
    Task<ServiceResult> OrdersAsync(Guid restaurantId, ClaimsPrincipal actor);
    Task<ServiceResult> UpdateStatusAsync(Guid id, ChangeOrderStatusRequest request, ClaimsPrincipal actor);
    Task<ServiceResult> AssignRiderAsync(Guid id, AssignRiderRequest request, ClaimsPrincipal actor);
}
