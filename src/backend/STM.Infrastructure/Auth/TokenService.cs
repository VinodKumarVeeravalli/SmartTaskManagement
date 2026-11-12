using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using STM.Application.Interfaces.Services;
using STM.Domain.Auth;
using STM.Domain.Entities;
using System.Security.Claims;
using System.Text;

namespace STM.Infrastructure.Auth
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _settings;

        public TokenService(IOptions<JwtSettings> options)
        {
            _settings = options.Value;
        }

        public (string Token, DateTime ExpiresAt) CreateToken(
            ApplicationUser user,
            IEnumerable<string> roles)
        {
            var expiresAt = DateTime.UtcNow.AddMinutes(
                _settings.ExpiryMinutes);

            var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email ?? ""),
            new(JwtRegisteredClaimNames.Name,
                $"{user.FirstName} {user.LastName}"),
            new(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
        };

            claims.AddRange(
                roles.Select(role => new Claim("role", role)));

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_settings.Key));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiresAt,
                Issuer = _settings.Issuer,
                Audience = _settings.Audience,
                SigningCredentials = credentials
            };

            var handler = new JsonWebTokenHandler();

            return (handler.CreateToken(descriptor), expiresAt);
        }
    }
}
