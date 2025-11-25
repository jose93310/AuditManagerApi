using JS.AuditManager.Application.DTO.Auth;
using JS.AuditManager.Application.IService;
using JS.AuditManager.Domain.ModelEntity;
using Microsoft.AspNetCore.Mvc;

namespace JS.AuditManager.RestApi.Controllers
{
    /// <summary>
    /// Controlador de autenticación.
    /// Gestiona el acceso, renovación y revocación de tokens para usuarios.
    /// </summary>
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// Constructor que inyecta el servicio de autenticación.
        /// </summary>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        #region Login
        /// <summary>
        /// Autentica a un usuario usando credenciales válidas.
        /// Retorna un Access Token y un Refresh Token si la autenticación es exitosa.
        /// </summary>
        /// <param name="request">Credenciales de acceso (usuario y contraseña).</param>
        /// <returns>Respuesta HTTP con tokens o mensaje de error.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            var result = await _authService.LoginAsync(request);

            if (result == null || result.DidError)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, result);
               
            }

            return StatusCode(StatusCodes.Status200OK, result);

            //return StatusCode((int)result.HttpResponseStatus, result);
        }
        #endregion
    }
}
