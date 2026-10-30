using System;
using System.Collections.Generic;
using System.Text;

namespace STM.Application.DTO.Auth
{
    public class AuthResponseDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
    }
}
