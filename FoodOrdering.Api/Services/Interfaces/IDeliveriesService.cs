using System.Security.Claims;
using FoodOrdering.Api.Models.Entities;
using FoodOrdering.Api.Models.Requests;

namespace FoodOrdering.Api.Services.Interfaces;

public interface IDeliveriesService
{
    Task<ServiceResult> GetAsync(ClaimsPrincipal actor);
    Task<ServiceResult> UpdateStatusAsync(Guid id, ChangeOrderStatusRequest request, ClaimsPrincipal actor);
}
