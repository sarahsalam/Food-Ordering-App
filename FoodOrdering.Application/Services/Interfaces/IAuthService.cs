using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.DTOs.Responses;

namespace FoodOrdering.Application.Services.Interfaces;

public interface IAuthService
{
    Task<(bool Success, string? Error)> RegisterAsync(
        RegisterRequest request);
    
    Task<(bool Success, string? Error, string? AccessToken, string? RefreshToken, UserResponse? User)> LoginAsync(
        LoginRequest request);

    Task<(bool Success, string? Error, string? AccessToken, string? RefreshToken)>
        RefreshAsync(string refreshToken);

    Task LogoutAsync(string refreshToken);
}
