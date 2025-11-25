using JS.AuditManager.Application.DTO.User;
using JS.AuditManager.Application.Helper.Security;
using JS.AuditManager.Application.IService;
using JS.AuditManager.Domain.Enum;
using JS.AuditManager.Domain.Interface;
using JS.AuditManager.Domain.IRepository;
using JS.AuditManager.Domain.ModelEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JS.AuditManager.Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #region RegisterUserAsync
        /// <summary>
        /// Crea un usuario
        /// </summary>
        /// <param name="request">Datos basicos del usuario para su creación</param>
        /// <param name="performedBy">Id del usuario que ejecuta la accion</param>
        /// <returns>Datos del usuario creado</returns>
        public async Task<ISingleResponse<UserRegisterResponseDTO>> RegisterUserAsync(UserRegisterRequestDTO request, Guid performedBy)
        {
            var response = new SingleResponse<UserRegisterResponseDTO>();

            try
            {            

                var userNameLower = request.Username.ToLower();
                var existingUser = await _userRepository.GetByUsernameAsync(userNameLower);
                if (existingUser != null)
                {
                    response.DidError = true;
                    response.ErrorMessage = "El nombre de usuario ya existe.";
                    return response;
                }

                var hashedPassword = PasswordHelper.Hash(request.Password);

                var newUser = new User
                {
                    UserId = Guid.NewGuid(),
                    UserName = userNameLower,
                    PasswordHash = hashedPassword,
                    LastPwdChanged = DateTime.UtcNow,
                    PasswordExpiration = DateTime.UtcNow.AddDays(90),
                    Email = request.Email,
                    Phone = request.Phone,
                    StatusId = 1,
                    UnsuccessfulSignin = 0,
                    CreatedBy = performedBy,
                    CreatedAt = DateTime.UtcNow
                };

                await _userRepository.CreateAsync(newUser);

                response.Model = new UserRegisterResponseDTO
                {
                    UserId = newUser.UserId,
                    Username = newUser.UserName
                };

                response.Message = "Usuario creado satisfactoriamente.";
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = $"Error al crear el usuario: {ex.Message}";
            }

            return response;
        }
        #endregion
        
        #region UpdateUserAsync
        /// <summary>
        /// Actualiza los campos editables de un usuario, teléfono, correo electrónico, estado y trazabilidad. 
        /// Antes de aplicar los cambios, valida que tanto el correo como el número de teléfono no estén registrados 
        /// por otro usuario en la base de datos, ya que son claves para mecanismos de autenticación y recuperación de contraseña.
        /// </summary>
        /// <param name="userDto">Objeto <see cref="UserUpdateDTO"/> que contiene los nuevos valores para el usuario.</param>
        /// <param name="performedBy">Id del usuario que ejecuta la modificación, utilizado para trazabilidad.</param>
        /// <returns>
        /// Una instancia de <see cref="IResponse"/> que indica si la operación fue exitosa o si ocurrió un conflicto
        /// de unicidad. Incluye mensaje descriptivo y estado HTTP correspondiente.
        /// </returns>
        public async Task<IResponse> UpdateUserAsync(UserUpdateDTO userDto, Guid performedBy)
        {
            var response = new ResponseModel();

            try
            {              

                var userModify = await _userRepository.GetByIdAsync(userDto.UserId);
                if (userModify == null)
                {
                    response.DidError = true;
                    response.ErrorMessage = "Usuario no encontrado.";
            
                    return response;
                }

                userModify.Email = userDto.Email;
                userModify.Phone = userDto.Phone;
                userModify.StatusId = userDto.StatusId;
                userModify.ModifiedBy = performedBy;
                userModify.ModifiedAt = DateTime.UtcNow;

                await _userRepository.UpdateUserAsync(userModify);

                response.Message = "Usuario actualizado correctamente.";        
            }
            catch (InvalidOperationException ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;           
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = $"Error inesperado al actualizar el usuario. Detalle:  {ex.Message}";
            }

            return response;
        }
        #endregion

        #region UpdateUserPasswordAsync
        /// <summary>
        /// Actualiza la contraseña de un usuario autenticado desde su perfil o backend.
        /// Requiere el identificador del usuario, el nuevo hash y el usuario que ejecuta la acción.
        /// </summary>
        /// <param name="userId">Identificador del usuario.</param>
        /// <param name="newHash">Nuevo hash de contraseña.</param>
        /// <param name="updatedBy">Usuario que ejecuta la acción.</param>
        /// <returns>
        /// Una instancia de <see cref="IResponse"/> indicando el resultado de la operación.
        /// </returns>
        public async Task<IResponse> UpdateUserPasswordAsync(Guid userId, string newHash, Guid updatedBy)
        {
            var response = new ResponseModel();

            try
            {
                var success = await _userRepository.UpdateUserPasswordAsync(userId, newHash, updatedBy);
                if (!success)
                {
                    response.DidError = true;
                    response.ErrorMessage = "Usuario no encontrado o no se pudo actualizar la contraseña.";          
                    return response;
                }

                response.Message = "Contraseña actualizada correctamente.";            
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = $"Error inesperado al actualizar la contraseña. Detalle: {ex.Message}";        
            }

            return response;
        }
        #endregion

    }
}
