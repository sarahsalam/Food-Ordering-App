using FoodOrdering.Application.DTOs.Responses;

namespace FoodOrdering.Application.Services.Interfaces;

public interface IUserService
{
    Task<List<UserResponse>> GetAllAsync();
    Task<UserResponse?> GetByIdAsync(Guid id);
}
