using System.Security.Claims;

namespace JS.AuditManager.Application.DTO.Auth
{
    public class AccessTokenResultDTO
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public string Jti { get; set; } = string.Empty;
        public List<Claim> Claims { get; set; } = new();
    }
}
