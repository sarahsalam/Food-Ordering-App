using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;

namespace FoodOrdering.Api.Services.Interfaces;

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
