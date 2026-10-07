using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.Mediator.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var result = await _mediator.Send(new RegisterAuthCommand(request));

        if (!result.Success)
        {
            return BadRequest(new
            {
                message = result.Error ?? "Registration failed."
            });
        }

        return Ok(new
        {
            message = "User registered successfully"
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await _mediator.Send(new LoginAuthCommand(request));

        if (!result.Success)
        {
            return Unauthorized(new
            {
                message = result.Error ?? "Invalid email or password."
            });
        }

        return Ok(new
        {
            message = "Login successful",
            accessToken = result.AccessToken,
            refreshToken = result.RefreshToken,
            user = result.User
        });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutRequest request)
    {
        await _mediator.Send(new LogoutAuthCommand(request));

        return Ok(new { message = "Logout successful" });
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
    {
        var result = await _mediator.Send(new RefreshAuthCommand(request));

        if (!result.Success)
        {
            return Unauthorized(new { message = result.Error ?? "The refresh token is invalid or expired." ?? "Unable to complete Refresh for Auth." });
        }

        return Ok(new
        {
            message = "Access token refreshed successfully.",
            accessToken = result.AccessToken,
            refreshToken = result.RefreshToken
        });
    }
}
