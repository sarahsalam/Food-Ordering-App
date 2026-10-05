using FoodOrdering.Api.Data;
using FoodOrdering.Api.Models.Entities;
using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Repositories.Interfaces;
using FoodOrdering.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodOrdering.Api.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly AppDbContext _context;

    public AuthService(UserManager<ApplicationUser> userManager,
        IUserRepository userRepository,
        IConfiguration configuration, AppDbContext context)
    {
        _userManager = userManager;
        _userRepository = userRepository;
        _configuration = configuration;
        _context = context;
    }

    public async Task<(bool Success, string? Error)> RegisterAsync(
        RegisterRequest request)
    {
        var existingUser = await _userRepository.GetByEmailAsync(
            request.Email);

        if (existingUser != null)
        {
            return (false, "A user with this email already exists.");
        }

        Guid? roleId;
        var configuredRoleId = _configuration["Registration:DefaultRoleId"];
        if (!string.IsNullOrWhiteSpace(configuredRoleId))
        {
            if (!Guid.TryParse(configuredRoleId, out var configuredId))
            {
                return (false, "The default registration role is not configured correctly.");
            }
            roleId = configuredId;
        }
        else
        {
            roleId = await _context.RbacInitializations
                .Select(item => item.DefaultRegistrationRoleId).SingleOrDefaultAsync();
        }
        var roleName = await _context.Roles.Where(role => role.Id == roleId)
            .Select(role => role.Name).SingleOrDefaultAsync();
        if (roleName == null)
        {
            return (false, "Configure Registration:DefaultRoleId with an existing role ID.");
        }

        var user = new ApplicationUser
        {
            FullName = request.FullName,
            Email = request.Email,
            UserName = request.Email
        };

        var result = await _userManager.CreateAsync(
            user,
            request.Password);

        if (!result.Succeeded)
        {
            var error = string.Join(
                ", ",
                result.Errors.Select(error => error.Description));

            return (false, error);
        }

        var roleResult = await _userManager.AddToRoleAsync(user, roleName);
        if (!roleResult.Succeeded)
        {
            var deleteResult = await _userManager.DeleteAsync(user);
            if (!deleteResult.Succeeded)
            {
                throw new InvalidOperationException("Could not clean up incomplete registration.");
            }
            return (false, string.Join(", ", roleResult.Errors.Select(error => error.Description)));
        }

        return (true, null);
    }

    public async Task<(bool Success, string? Error, string? AccessToken, string? RefreshToken, UserResponse? User)>
        LoginAsync(
            LoginRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(
            request.Email);

        if (user == null)
        {
            return (false, "Invalid email or password.", null, null, null);
        }

        var passwordValid = await _userManager.CheckPasswordAsync(
            user,
            request.Password);

        if (!passwordValid)
        {
            return (false, "Invalid email or password.", null, null, null);
        }

        var key = _configuration["Jwt:Key"]!;
        var issuer = _configuration["Jwt:Issuer"]!;
        var audience = _configuration["Jwt:Audience"]!;
        var expiryMinutes = int.Parse(
            _configuration["Jwt:ExpiryMinutes"]!);

        var claims = new List<Claim>
        {
            new Claim(
                ClaimTypes.NameIdentifier,
                user.Id.ToString()),

            new Claim(
                ClaimTypes.Email,
                user.Email!)
        };

        await AddAuthorizationClaimsAsync(user, claims);

        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(key));

        var credentials = new SigningCredentials(
            securityKey,
            SecurityAlgorithms.HmacSha256);

        var tokenString = CreateAccessToken(
            claims,
            issuer,
            audience,
            expiryMinutes,
            credentials);

        var refreshToken = CreateRefreshToken(user.Id);
        _userRepository.AddRefreshToken(refreshToken.Entity);
        await _userRepository.SaveRefreshTokensAsync();

        return (true, null, tokenString, refreshToken.RawToken, UserService.Map(user));
    }

    public async Task<(bool Success, string? Error, string? AccessToken, string? RefreshToken)>
        RefreshAsync(string refreshToken)
    {
        var tokenHash = HashRefreshToken(refreshToken);
        var storedToken = await _userRepository.GetRefreshTokenAsync(tokenHash);

        if (storedToken == null || storedToken.RevokedAt.HasValue ||
            storedToken.ExpiresAt <= DateTime.UtcNow)
        {
            return (false, "Invalid or expired refresh token.", null, null);
        }

        storedToken.RevokedAt = DateTime.UtcNow;

        var user = storedToken.User;
        var key = _configuration["Jwt:Key"]!;
        var issuer = _configuration["Jwt:Issuer"]!;
        var audience = _configuration["Jwt:Audience"]!;
        var expiryMinutes = int.Parse(_configuration["Jwt:ExpiryMinutes"]!);
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email!)
        };
        await AddAuthorizationClaimsAsync(user, claims);

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            SecurityAlgorithms.HmacSha256);

        var accessToken = CreateAccessToken(
            claims,
            issuer,
            audience,
            expiryMinutes,
            credentials);
        var newRefreshToken = CreateRefreshToken(user.Id);
        _userRepository.AddRefreshToken(newRefreshToken.Entity);
        await _userRepository.SaveRefreshTokensAsync();

        return (true, null, accessToken, newRefreshToken.RawToken);
    }

    public async Task LogoutAsync(string refreshToken)
    {
        var storedToken = await _userRepository.GetRefreshTokenAsync(HashRefreshToken(refreshToken));
        if (storedToken == null || storedToken.RevokedAt.HasValue)
        {
            return;
        }

        storedToken.RevokedAt = DateTime.UtcNow;
        await _userRepository.SaveRefreshTokensAsync();
    }

    private async Task AddAuthorizationClaimsAsync(ApplicationUser user, List<Claim> claims)
    {
        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }


    }

    private string CreateAccessToken(
        IEnumerable<Claim> claims,
        string issuer,
        string audience,
        int expiryMinutes,
        SigningCredentials credentials)
    {
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private (RefreshToken Entity, string RawToken) CreateRefreshToken(Guid userId)
    {
        var rawToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64))
            .Replace("+", "-")
            .Replace("/", "_")
            .TrimEnd('=');
        var expiryDays = int.Parse(
            _configuration["Jwt:RefreshTokenExpiryDays"] ?? "7");

        return (new RefreshToken
        {
            UserId = userId,
            Token = HashRefreshToken(rawToken),
            ExpiresAt = DateTime.UtcNow.AddDays(expiryDays)
        }, rawToken);
    }

    private static string HashRefreshToken(string token)
    {
        return Convert.ToHexString(
            SHA256.HashData(Encoding.UTF8.GetBytes(token)));
    }
}
