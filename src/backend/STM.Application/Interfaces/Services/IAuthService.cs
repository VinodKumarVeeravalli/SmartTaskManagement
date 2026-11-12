using STM.Application.DTO.Auth;
using STM.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace STM.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<RegisterResponseDto> RegisterAsync(
            RegisterRequestDto request);

        Task<AuthResponseDto?> LoginAsync(
            LoginRequestDto request);
    }
}
