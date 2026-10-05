using System.Security.Claims;
using FoodOrdering.Api.Models.Entities;
using FoodOrdering.Api.Models.Requests;

namespace FoodOrdering.Api.Services.Interfaces;

public interface IMyCartService
{
    Task<ServiceResult> GetAsync(ClaimsPrincipal actor);
}
