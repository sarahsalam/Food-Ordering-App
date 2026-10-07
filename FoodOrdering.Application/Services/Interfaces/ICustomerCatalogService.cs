
namespace FoodOrdering.Application.Services.Interfaces;

public interface ICustomerCatalogService
{
    Task<ServiceResult> GetAsync(ClaimsPrincipal actor);
}
