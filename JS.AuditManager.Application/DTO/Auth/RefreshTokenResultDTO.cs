namespace JS.AuditManager.Application.DTO.Auth
{
    public class RefreshTokenResultDTO
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
    }
}
