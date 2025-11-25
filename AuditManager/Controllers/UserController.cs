using Asp.Versioning;
using JS.AuditManager.Application.DTO.User;
using JS.AuditManager.Application.Helper.Security;
using JS.AuditManager.Application.IService;
using JS.AuditManager.Domain.Enum;
using JS.AuditManager.Domain.ModelEntity;
using Microsoft.AspNetCore.Mvc;

namespace JS.AuditManager.RestApi.Controllers
{
    /// <summary>
    /// Controlador de usuarios.
    /// Gestiona la creación y registro de nuevos usuarios
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Constructor que inyecta el servicio de usuarios.
        /// </summary>
        /// <param name="userService">Servicio de lógica de negocio para usuarios.</param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        #region RegisterUser
        /// <summary>
        /// Registra un nuevo usuario en el sistema.
        /// Este endpoint está público por fines de prueba
        /// Se debe protegerlo con JWT y políticas personalizadas.
        /// </summary>
        /// <param name="request">Datos del nuevo usuario (nombre, correo, contraseña, etc.).</param>
        /// <returns>Respuesta con estado HTTP y resultado del registro.</returns>
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequestDTO request)
        {
            var userId = TokenHelper.GetUserId(User);
            if (userId == null)
            {
                var response = new SingleResponse<bool>();

                response.DidError = true;
                response.ErrorMessage = "Identificador del usuario no valido.";

                return StatusCode(StatusCodes.Status401Unauthorized, response);
            }
            var result = await _userService.RegisterUserAsync(request, userId.Value);
          
            return StatusCode(StatusCodes.Status201Created, result);
        }
        #endregion

        
        #region UpdateUser
        /// <summary>
        /// Actualiza los datos editables de un usuario.
        /// </summary>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDTO userDto)
        {
            var updatedBy = TokenHelper.GetUserId(User);
            if (updatedBy == null)
            {
                var response = new SingleResponse<bool>();

                response.DidError = true;
                response.ErrorMessage = "Identificador del usuario no valido.";
                return StatusCode(StatusCodes.Status401Unauthorized, response);
            }

            var result = await _userService.UpdateUserAsync(userDto, updatedBy.Value);
            return StatusCode(StatusCodes.Status200OK, result);
        }
        #endregion

        #region UpdateUserPassword
        /// <summary>
        /// Actualiza la contraseña de un usuario.
        /// </summary>
        [HttpPut("{userId}/password")]
        public async Task<IActionResult> UpdateUserPassword(Guid userId, [FromBody] string newPassword)
        {
            var updatedBy = TokenHelper.GetUserId(User);
            if (updatedBy == null)
            {
                var response = new SingleResponse<bool>();

                response.DidError = true;
                response.ErrorMessage = "Identificador del usuario no valido.";

                return StatusCode(StatusCodes.Status401Unauthorized, response);
            }

            var newHash = PasswordHelper.Hash(newPassword);

            var result = await _userService.UpdateUserPasswordAsync(userId, newHash, updatedBy.Value);
            return StatusCode(StatusCodes.Status200OK, result);
        }
        #endregion
    }
}
