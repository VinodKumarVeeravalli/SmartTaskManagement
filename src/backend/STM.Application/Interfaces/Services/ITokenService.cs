using STM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace STM.Application.Interfaces.Services
{
    public interface ITokenService
    {
        (string Token, DateTime ExpiresAt) CreateToken(
            ApplicationUser user,
            IEnumerable<string> roles);
    }
}
