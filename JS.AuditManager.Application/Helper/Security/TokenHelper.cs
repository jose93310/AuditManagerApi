using JS.AuditManager.Application.DTO.Auth;
using JS.AuditManager.Domain.ModelEntity;
using System.Security.Claims;

namespace JS.AuditManager.Application.Helper.Security
{
    /// <summary>
    /// Utilidades para generar instancia del token y para extraer información del token JWT
    /// </summary>
    public static class TokenHelper
    {

        #region CreateTokenEntity
        /// <summary>
        /// Crea una instancia del token
        /// </summary>
        /// <param name="user"></param>
        /// <param name="access"></param>
        /// <param name="refresh"></param>
        /// <returns>Devuelve una instancia del token</returns>
        public static Token CreateTokenEntity(User user, AccessTokenResultDTO access, RefreshTokenResultDTO refresh)
        {
            return new Token
            {
                TokenId = Guid.Parse(access.Jti),
                UserId = user.UserId,
                AccessToken = access.Token,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = access.ExpiresAt,
                RefreshToken = refresh.Token,
                RefreshExpiresAt = refresh.ExpiresAt,
                IsRevoked = false
            };
        }
        #endregion

        #region GetUsername
        /// <summary>
        /// Obtiene el nombre de usuario desde el claim 'username'.
        /// </summary>
        /// <param name="user">Datos del usuario propietario del token</param>
        /// <returns>retorna un string con el username propietario del token, en caso de no existir retorna null</returns>
        public static string? GetUsername(ClaimsPrincipal user)
        {
            return user.FindFirst("username")?.Value;
        }
        #endregion

        #region GetUserId
        /// <summary>
        /// Obtiene el identificador único del usuario desde el claim estándar 'NameIdentifier'. 
        /// </summary>
        /// <param name="user">Datos del usuario propietario del token</param>
        /// <returns>retorna un guid con el userId propietario del token, en caso de no existir retorna null</returns>
        public static Guid? GetUserId(ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(userIdClaim, out var userId) ? userId : null;
        }
        #endregion

        #region IsAuthenticated
        /// <summary>
        /// Verifica si el token contiene un usuario válido.
        /// </summary>
        /// <param name="user">Datos del usuario propietario del token</param>
        /// <returns>true si esta autenticado o false</returns>
        public static bool IsAuthenticated(ClaimsPrincipal user)
        {
            return user?.Identity?.IsAuthenticated == true;
        }
        #endregion

        

    }
}
