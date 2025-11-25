using JS.AuditManager.Domain.IRepository;
using JS.AuditManager.Domain.ModelEntity;
using JS.AuditManager.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace JS.AuditManager.Infrastructure.Auth
{
    public class TokenRepository : ITokenRepository
    {
        private readonly AuditContext _context;

        public TokenRepository(AuditContext context)
        {
            _context = context;
        }

        #region CreateAsync
        /// <summary>
        /// Registra un nuevo token en la base de datos.
        /// </summary>
        /// <param name="token">Entidad Token que contiene el JWT, refresh token y datos de sesión.</param>
        public async Task CreateAsync(Token token)
        {
            await _context.Set<Token>().AddAsync(token);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region GetByRefreshTokenAsync
        /// <summary>
        /// Busca un token activo (no revocado ni expirado) mediante el valor del refresh token.
        /// </summary>
        /// <param name="refreshToken">Cadena codificada que representa el refresh token único.</param>
        /// <returns>La entidad Token si es válida y vigente; null si no se encuentra.</returns>
        public async Task<Token?> GetByRefreshTokenAsync(string refreshToken)
        {
            return await _context.Set<Token>()
                .FirstOrDefaultAsync(t =>
                    t.RefreshToken == refreshToken &&
                    !t.IsRevoked &&
                    t.RefreshExpiresAt > DateTime.UtcNow);
        }
        #endregion

        #region RevokeAsync
        /// <summary>
        /// Marca un token específico como revocado en la base de datos.
        /// Esto evita que pueda utilizarse nuevamente, por ejemplo durante logout o revocación de sesión.
        /// </summary>
        /// <param name="tokenId">Identificador único del token a revocar.</param>
        public async Task RevokeAsync(Guid tokenId)
        {
            var token = await _context.Set<Token>().FindAsync(tokenId);
            if (token != null && !token.IsRevoked)
            {
                token.IsRevoked = true;
                //token.RevokedAt = DateTime.UtcNow;
                _context.Update(token);
                await _context.SaveChangesAsync();
            }
        }
        #endregion

        #region GetByUserIdAsync
        /// <summary>
        /// Retorna todos los tokens activos del usuario (sin revocar y sin expirar).
        /// </summary>
        public async Task<List<Token>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Set<Token>()
                .Where(t =>
                    t.UserId == userId &&
                    !t.IsRevoked &&
                    t.ExpiresAt > DateTime.UtcNow)
                .ToListAsync();
        }
        #endregion

    }
}
