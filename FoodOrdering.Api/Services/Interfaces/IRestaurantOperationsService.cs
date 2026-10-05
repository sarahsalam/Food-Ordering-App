using System.Security.Claims;
using FoodOrdering.Api.Models.Entities;
using FoodOrdering.Api.Models.Requests;

namespace FoodOrdering.Api.Services.Interfaces;

public interface IRestaurantOperationsService
{
    Task<ServiceResult> OrdersAsync(Guid restaurantId, ClaimsPrincipal actor);
    Task<ServiceResult> UpdateStatusAsync(Guid id, ChangeOrderStatusRequest request, ClaimsPrincipal actor);
    Task<ServiceResult> AssignRiderAsync(Guid id, AssignRiderRequest request, ClaimsPrincipal actor);
}
