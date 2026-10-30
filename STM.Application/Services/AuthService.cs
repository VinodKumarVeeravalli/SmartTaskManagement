using Microsoft.AspNetCore.Identity;
using STM.Application.DTO.Auth;
using STM.Application.DTOs.Auth;
using STM.Application.Interfaces.Services;
using STM.Domain.Entities;

namespace STM.Application.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenService _tokenService;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task<RegisterResponseDto> RegisterAsync(
        RegisterRequestDto request)
    {
        var existingUser =
        await _userManager.FindByEmailAsync(request.Email);

        if (existingUser != null)
        {
            throw new InvalidOperationException(
                "Email already registered.");
        }

        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        var result = await _userManager.CreateAsync(
            user, request.Password);

        if (!result.Succeeded)
        {
            throw new InvalidOperationException(
                string.Join("; ",
                    result.Errors.Select(e => e.Description)));
        }

        var roleResult = await _userManager.AddToRoleAsync(
            user, Roles.Developer);

        if (!roleResult.Succeeded)
        {
            // Compensate for failed role assignment.
            var deleteResult = await _userManager.DeleteAsync(user);

            throw new InvalidOperationException(
                "Role assignment failed: " +
                string.Join("; ",
                    roleResult.Errors.Select(e => e.Description)));
        }

        return new RegisterResponseDto
        {
            UserId = user.Id,
            Email = user.Email!,
            Message = "Registration successful. Please login."
        };
    }

    public async Task<AuthResponseDto?> LoginAsync(
        LoginRequestDto request)
    {
        var user = await _userManager
            .FindByEmailAsync(request.Email);

        if (user == null)
            return null;

        var result = await _signInManager
            .CheckPasswordSignInAsync(
                user,
                request.Password,
                lockoutOnFailure: true);

        if (!result.Succeeded)
            return null;

        var roles = await _userManager.GetRolesAsync(user);

        var (token, expiresAt) =
            _tokenService.CreateToken(user, roles);

        return new AuthResponseDto
        {
            AccessToken = token,
            ExpiresAt = expiresAt
        };
    }
}