using JS.AuditManager.Domain.ModelEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JS.AuditManager.Domain.IRepository
{
    public interface IUserRepository
    {
        #region GetByUsernameAsync
        /// <summary>
        /// Obtiene un usuario por su nombre de usuario, incluyendo sus grupos asociados.
        /// </summary>
        /// <param name="username">Nombre de usuario a buscar.</param>
        /// <returns>Usuario con sus grupos, o null si no existe.</returns>
        Task<User?> GetByUsernameAsync(string username);
        #endregion

        #region GetByIdAsync
        /// <summary>
        /// Obtiene un usuario por su identificador único (UserId).
        /// </summary>
        /// <param name="userId">GUID del usuario.</param>
        /// <returns>Usuario si se encuentra; null si no existe.</returns>
        Task<User?> GetByIdAsync(Guid userId);
        #endregion

        #region ValidatePasswordAsync
        /// <summary>
        /// Valida una contraseña comparándola con su hash almacenado usando BCrypt.
        /// </summary>
        /// <param name="user">Entidad del usuario que contiene el hash.</param>
        /// <param name="password">Contraseña en texto plano a validar.</param>
        /// <returns>Verdadero si coincide; falso si es incorrecta.</returns>
        Task<bool> ValidatePasswordAsync(User user, string password);
        #endregion

        #region UpdateLoginAttemptsAsync
        /// <summary>
        /// Actualiza los intentos fallidos de login para un usuario.
        /// Se puede reiniciar a cero o incrementar en uno.
        /// </summary>
        /// <param name="user">Usuario a actualizar.</param>
        /// <param name="reset">Verdadero para reiniciar el contador; falso para incrementar.</param>
        Task UpdateLoginAttemptsAsync(User user, bool reset);
        #endregion

        #region CreateAsync
        /// <summary>
        /// Crea un nuevo usuario en la base de datos.
        /// </summary>
        /// <param name="user">Entidad User con datos para persistencia.</param>
        Task CreateAsync(User user);
        #endregion

        #region UpdateUserAsync
        /// <summary>
        /// Actualiza los datos de un usuario
        /// </summary>
        /// <param name="user">Entidad del usuario con los datos modificados.</param>
        Task<bool> UpdateUserAsync(User user);
        #endregion

        #region UpdateUserPasswordAsync
        /// <summary>
        /// Actualiza la contraseña de un usuario.
        /// </summary>
        /// <param name="userId">Identificador del usuario.</param>
        /// <param name="newHash">Nuevo hash de contraseña.</param>
        /// <param name="modifiedBy">Id del usuario que ejecuta la acción.</param>
        Task<bool> UpdateUserPasswordAsync(Guid userId, string newHash, Guid modifiedBy);
        #endregion


    }
}
