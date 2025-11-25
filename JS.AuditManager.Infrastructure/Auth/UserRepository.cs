using JS.AuditManager.Domain.IRepository;
using JS.AuditManager.Domain.ModelEntity;
using JS.AuditManager.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JS.AuditManager.Infrastructure.Auth
{
    /// <summary>
    /// Repositorio para operaciones relacionadas con la entidad User.
    /// Acceso y manipulación de usuarios
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly AuditContext _context;

        /// <summary>
        /// Inicializa el repositorio con el contexto de base de datos.
        /// </summary>
        public UserRepository(AuditContext context)
        {
            _context = context;
        }

        #region GetByUsernameAsync
        /// <summary>
        /// Obtiene un usuario por su nombre de usuario, incluyendo sus grupos asociados.
        /// </summary>
        /// <param name="username">Nombre de usuario a buscar.</param>
        /// <returns>Usuario con sus grupos, o null si no existe.</returns>
        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == username);
        }
        #endregion

        #region GetByIdAsync
        /// <summary>
        /// Obtiene un usuario por su identificador único (UserId).
        /// </summary>
        /// <param name="userId">GUID del usuario.</param>
        /// <returns>Usuario si se encuentra; null si no existe.</returns>
        public async Task<User?> GetByIdAsync(Guid userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        }
        #endregion

        #region ValidatePasswordAsync
        /// <summary>
        /// Valida una contraseña comparándola con su hash almacenado usando BCrypt.
        /// </summary>
        /// <param name="user">Entidad del usuario que contiene el hash.</param>
        /// <param name="password">Contraseña en texto plano a validar.</param>
        /// <returns>Verdadero si coincide; falso si es incorrecta.</returns>
        public Task<bool> ValidatePasswordAsync(User user, string password)
        {
            return Task.FromResult(BCrypt.Net.BCrypt.Verify(password, user.PasswordHash));
        }
        #endregion

        #region UpdateLoginAttemptsAsync
        /// <summary>
        /// Actualiza los intentos fallidos de login para un usuario.
        /// Se puede reiniciar a cero o incrementar en uno.
        /// </summary>
        /// <param name="user">Usuario a actualizar.</param>
        /// <param name="reset">Verdadero para reiniciar el contador; falso para incrementar.</param>
        public async Task UpdateLoginAttemptsAsync(User user, bool reset)
        {
            //user.UnsuccessfulSignin = reset ? 0 : user.UnsuccessfulSignin + 1;
            user.UnsuccessfulSignin = reset ? (byte)0 : (byte)(user.UnsuccessfulSignin + 1);

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region CreateAsync
        /// <summary>
        /// Crea un nuevo usuario en la base de datos.
        /// </summary>
        /// <param name="user">Entidad User con datos para persistencia.</param>
        public async Task CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        #endregion
       
        #region UpdateUserAsync
        /// <summary>
        /// Actualiza los campos editables de un usuario: teléfono, correo, estado y trazabilidad.
        /// Valida que tanto el correo electrónico como el número de teléfono sean únicos en la base de datos.
        /// </summary>
        /// <param name="user">Entidad del usuario con los datos modificados.</param>
        /// <returns>True si la actualización fue exitosa; false si hubo conflicto de datos únicos.</returns>
        public async Task<bool> UpdateUserAsync(User user)
        {
            // Validar unicidad de Email
            var emailExists = await _context.Users
                .AnyAsync(u => u.Email == user.Email && u.UserId != user.UserId);

            if (emailExists)
                throw new InvalidOperationException("El correo electrónico ya está registrado por otro usuario.");

            // Validar unicidad de Phone
            var phoneExists = await _context.Users
                .AnyAsync(u => u.Phone == user.Phone && u.UserId != user.UserId);

            if (phoneExists)
                throw new InvalidOperationException("El número de teléfono ya está registrado por otro usuario.");

            // Marcar propiedades modificadas
            _context.Entry(user).Property(u => u.Phone).IsModified = true;
            _context.Entry(user).Property(u => u.Email).IsModified = true;
            _context.Entry(user).Property(u => u.StatusId).IsModified = true;
            _context.Entry(user).Property(u => u.ModifiedBy).IsModified = true;
            _context.Entry(user).Property(u => u.ModifiedAt).IsModified = true;

            return await _context.SaveChangesAsync() > 0;
        }
        #endregion

        #region UpdateUserPasswordAsync
        /// <summary>
        /// Actualiza la contraseña y campos relacionados de un usuario.
        /// </summary>
        /// <param name="userId">Identificador del usuario.</param>
        /// <param name="newHash">Nuevo hash de contraseña.</param>
        /// <param name="updatedBy">ID del usuario que ejecuta la acción.</param>
        public async Task<bool> UpdateUserPasswordAsync(Guid userId, string newHash, Guid updatedBy)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            user.PasswordHash = newHash;
            user.LastPwdChanged = DateTime.UtcNow;
            user.PasswordExpiration = DateTime.UtcNow.AddMonths(6);
            user.ModifiedBy = updatedBy;
            user.ModifiedAt = DateTime.UtcNow;

            _context.Entry(user).Property(u => u.PasswordHash).IsModified = true;
            _context.Entry(user).Property(u => u.LastPwdChanged).IsModified = true;
            _context.Entry(user).Property(u => u.PasswordExpiration).IsModified = true;
            _context.Entry(user).Property(u => u.ModifiedBy).IsModified = true;
            _context.Entry(user).Property(u => u.ModifiedAt).IsModified = true;

            return await _context.SaveChangesAsync() > 0;
        }
        #endregion

    }
}
