using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.AuditManager.Application.DTO.User
{
    public class UserRegisterRequestDTO
    {
        /// <summary>
        /// Nombre de usuario requerido. Mínimo 4 caracteres.
        /// </summary>
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [MinLength(4, ErrorMessage = "El nombre de usuario debe tener al menos 4 caracteres.")]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Contraseña requerida. Mínimo 6 caracteres.
        /// </summary>
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Correo electrónico requerido. Debe tener formato válido.
        /// </summary>
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido.")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Número de teléfono requerido. Mínimo 10 caracteres.
        /// </summary>
        [Required(ErrorMessage = "El número de teléfono es obligatorio.")]
        [MinLength(10, ErrorMessage = "El número de teléfono debe tener al menos 10 caracteres.")]
        public string Phone { get; set; } = string.Empty;
    }
}
