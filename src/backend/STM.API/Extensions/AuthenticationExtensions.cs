using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using STM.Application.Interfaces.Services;
using STM.Domain.Auth;
using STM.Infrastructure.Auth;
using System.Text;

namespace STM.API.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtSettings = configuration
                .GetSection("Jwt")
                .Get<JwtSettings>()
                ?? throw new InvalidOperationException(
                    "JWT settings are missing.");

            if (string.IsNullOrWhiteSpace(jwtSettings.Key) ||
                Encoding.UTF8.GetByteCount(jwtSettings.Key) < 32 ||
                string.IsNullOrWhiteSpace(jwtSettings.Issuer) ||
                string.IsNullOrWhiteSpace(jwtSettings.Audience) ||
                jwtSettings.ExpiryMinutes <= 0)
            {
                throw new InvalidOperationException(
                    "JWT configuration is invalid.");
            }

            // Register JWT settings
            services.Configure<JwtSettings>(
                configuration.GetSection("Jwt"));

            // Register TokenService
            services.AddScoped<ITokenService, TokenService>();

            // Configure JWT validation
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.MapInboundClaims = false;

                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,

                            ValidIssuer = jwtSettings.Issuer,
                            ValidAudience = jwtSettings.Audience,

                            IssuerSigningKey =
                                new SymmetricSecurityKey(
                                    Encoding.UTF8.GetBytes(jwtSettings.Key)),

                            ClockSkew = TimeSpan.Zero,
                            NameClaimType = JwtRegisteredClaimNames.Name,
                            RoleClaimType = "role"
                        };
                });

            services.AddAuthorization();

            return services;
        }
    }
}
