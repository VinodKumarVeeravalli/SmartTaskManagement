using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STM.Application.DTO.Auth;
using STM.Application.DTOs.Auth;
using STM.Application.Interfaces.Services;

namespace STM.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult<RegisterResponseDto>> Register(
     [FromBody] RegisterRequestDto request)
    {
        var response = await _authService.RegisterAsync(request);

        return StatusCode(StatusCodes.Status201Created, response);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequestDto request)
    {
        var response = await _authService
            .LoginAsync(request);

        if (response == null)
            return Unauthorized(new
            {
                message = "Invalid credentials or sign-in unavailable."
            });

        return Ok(response);
    }
}