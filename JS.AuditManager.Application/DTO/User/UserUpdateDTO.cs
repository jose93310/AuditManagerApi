using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.AuditManager.Application.DTO.User
{
    public class UserUpdateDTO
    {
        /// <summary>
        /// Id del usuario a actualizar
        /// </summary>        
        [Required(ErrorMessage = "El identificador del usuario es obligatorio.")]
        public Guid UserId { get; set; }

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

        /// <summary>
        /// Estatus del usuario
        /// </summary>       
        public byte StatusId { get; set; }
    }
}
