using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.AuditManager.Application.Helper.Security
{
    /// <summary>
    /// Helper para operaciones de seguridad relacionadas con contraseñas.
    /// </summary>
    public static class PasswordHelper
    {
        /// <summary>
        /// Genera un hash seguro para una contraseña usando BCrypt.
        /// </summary>
        /// <param name="plainPassword">Contraseña en texto plano.</param>
        /// <returns>Hash seguro de la contraseña.</returns>
        public static string Hash(string plainPassword)
        {
            return BCrypt.Net.BCrypt.HashPassword(plainPassword);
        }

        /// <summary>
        /// Verifica si una contraseña en texto plano coincide con un hash almacenado.
        /// </summary>
        /// <param name="plainPassword">Contraseña ingresada por el usuario.</param>
        /// <param name="hashedPassword">Hash almacenado en la base de datos.</param>
        /// <returns>True si coinciden; false si no.</returns>
        public static bool Verify(string plainPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
        }
    }
}
