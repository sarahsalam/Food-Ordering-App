using System.Security.Claims;
using FoodOrdering.Api.Models.Entities;
using FoodOrdering.Api.Models.Requests;

namespace FoodOrdering.Api.Services.Interfaces;

public interface ICustomerCatalogService
{
    Task<ServiceResult> GetAsync(ClaimsPrincipal actor);
}
