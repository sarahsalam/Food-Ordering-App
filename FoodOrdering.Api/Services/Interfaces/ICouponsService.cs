using System.Security.Claims;
using FoodOrdering.Api.Models.Entities;
using FoodOrdering.Api.Models.Requests;

namespace FoodOrdering.Api.Services.Interfaces;

public interface ICouponsService
{
    Task<ServiceResult> GetAsync(ClaimsPrincipal actor);
    Task<ServiceResult> CreateAsync(CreateCouponRequest request, ClaimsPrincipal actor);
    Task<ServiceResult> SetAvailabilityAsync(Guid id, CouponAvailabilityRequest request, ClaimsPrincipal actor);
    Task<ServiceResult> AssignAsync(Guid couponId, Guid userId, ClaimsPrincipal actor);
    Task<ServiceResult> UsagesAsync(Guid couponId, ClaimsPrincipal actor);
}
