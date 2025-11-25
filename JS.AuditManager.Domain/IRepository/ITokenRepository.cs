using JS.AuditManager.Domain.ModelEntity;

namespace JS.AuditManager.Domain.IRepository
{
    public interface ITokenRepository
    {
        #region CreateAsync
        /// <summary>
        /// Registra un nuevo token en la base de datos.
        /// </summary>
        /// <param name="token">Entidad Token que contiene el JWT, refresh token, y metadatos de sesión.</param>
        Task CreateAsync(Token token);
        #endregion

        #region GetByRefreshTokenAsync
        /// <summary>
        /// Busca un token activo (no revocado ni expirado) mediante el valor del refresh token.
        /// </summary>
        /// <param name="refreshToken">Cadena codificada que representa el refresh token único.</param>
        /// <returns>La entidad Token si es válida y vigente; null si no se encuentra.</returns>
        Task<Token?> GetByRefreshTokenAsync(string refreshToken);
        #endregion

        #region RevokeAsync
        /// <summary>
        /// Marca un token específico como revocado en la base de datos.
        /// Esto evita que pueda utilizarse nuevamente, por ejemplo durante logout o revocación de sesión.
        /// </summary>
        /// <param name="tokenId">Identificador único del token a revocar.</param>
        Task RevokeAsync(Guid tokenId);
        #endregion

        #region GetByUserIdAsync
        /// <summary>
        /// Obtiene todos los tokens activos (no revocados ni vencidos) asociados a un usuario.
        /// </summary>
        /// <param name="userId">Identificador del usuario.</param>
        Task<List<Token>> GetByUserIdAsync(Guid userId);
        #endregion

    }
}
