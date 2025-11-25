using JS.AuditManager.Application.DTO.Auth;
using JS.AuditManager.Domain.ModelEntity;

namespace JS.AuditManager.Application.IService
{
    public interface ITokenGeneratorService
    {
        AccessTokenResultDTO GenerateAccessToken(User user);
        RefreshTokenResultDTO GenerateRefreshToken();
    }
}
