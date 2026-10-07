
namespace FoodOrdering.Application.Services;

public class CustomerCatalogService : ICustomerCatalogService
{
    private readonly IRestaurantRepository _repository;

    public CustomerCatalogService(IRestaurantRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResult> GetAsync(ClaimsPrincipal actor)
    {
        var restaurants = (await _repository.GetAvailableAsync())
            .Select(item => new { item.Id, item.Name, item.Description, item.LogoUrl })
            .ToList();
        return ServiceResult.Ok(restaurants);
    }
}
