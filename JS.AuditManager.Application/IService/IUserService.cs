using JS.AuditManager.Application.DTO.User;
using JS.AuditManager.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.AuditManager.Application.IService
{
    public interface IUserService
    {
        #region RegisterUserAsync
        /// <summary>
        /// Registra un nuevo usuario
        /// </summary>
        /// <param name="userRegister">Datos del nuevo usuario a crear.</param>
        /// <param name="performedBy">ID del usuario que ejecuta la acción.</param>
        /// <returns>Respuesta estructurada con estado de la operación.</returns>
        Task<ISingleResponse<UserRegisterResponseDTO>> RegisterUserAsync(UserRegisterRequestDTO userRegister, Guid performedBy);
        #endregion

        #region UpdateUserAsync
        /// <summary>
        /// Actualiza los campos editables de un usuario: teléfono, correo, estado y trazabilidad.
        /// </summary>
        /// <param name="userDto">Datos modificables del usuario.</param>
        /// <param name="performedBy">ID del usuario que ejecuta la acción.</param>
        /// <returns>Respuesta estructurada con estado de la operación.</returns>
        Task<IResponse> UpdateUserAsync(UserUpdateDTO userDto, Guid performedBy);
        #endregion

        #region UpdateUserPasswordAsync
        /// <summary>
        /// Actualiza la contraseña y campos relacionados de un usuario.
        /// </summary>
        /// <param name="userId">Identificador del usuario.</param>
        /// <param name="newHash">Nuevo hash de contraseña.</param>
        /// <param name="updatedBy">Usuario que ejecuta la acción.</param>
        /// <returns>Respuesta estructurada con estado de la operación.</returns>
        Task<IResponse> UpdateUserPasswordAsync(Guid userId, string newHash, Guid updatedBy);
        #endregion


    }
}
