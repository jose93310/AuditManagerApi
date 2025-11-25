using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.AuditManager.Application.DTO.Auth
{
    public class LoginRequestDTO
    {
        /// Nombre de usuario requerido
        /// </summary>
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        public string? Username { get; set; }

        /// <summary>
        /// Contraseña requerida
        /// </summary>
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public string? Password { get; set; }
    }
}
