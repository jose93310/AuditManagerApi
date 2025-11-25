using JS.AuditManager.Application.DTO.Auth;
using JS.AuditManager.Application.Helper.Security;
using JS.AuditManager.Application.IService;
using JS.AuditManager.Domain.Enum;
using JS.AuditManager.Domain.Interface;
using JS.AuditManager.Domain.IRepository;
using JS.AuditManager.Domain.ModelEntity;

namespace JS.AuditManager.Application.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenGeneratorService _tokenGeneratorService;
        private readonly ITokenRepository _tokenRepository;
        public AuthService(IUserRepository userRepository, ITokenGeneratorService tokenGeneratorService, ITokenRepository tokenRepository)
        {
            _userRepository = userRepository;
            _tokenGeneratorService = tokenGeneratorService;
            _tokenRepository = tokenRepository;
        }

        #region LoginAsync
        /// <summary>
        /// Metodo de autenticación, valida el username y passswordHash
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Objeto ISingleResponse<LoginResponseDTO> con el resultado de la autenticacion</returns>
        public async Task<ISingleResponse<LoginResponseDTO>> LoginAsync(LoginRequestDTO request)
        {
            var response = new SingleResponse<LoginResponseDTO>();

            try
            {
                var userNameLower = request?.Username?.ToLower() ?? string.Empty;
                var user = await _userRepository.GetByUsernameAsync(userNameLower);

                if (user is null || string.IsNullOrWhiteSpace(request?.Password) || !PasswordHelper.Verify(request.Password, user.PasswordHash))
                {
                    response.DidError = true;
                    response.Message = "Credenciales inválidas.";
                    response.ErrorMessage = "Credenciales inválidas.";
                    return response;
                }


                var access = _tokenGeneratorService.GenerateAccessToken(user);
                var refresh = _tokenGeneratorService.GenerateRefreshToken();

                // helper para crear instancia de token
                var tokenEntity = TokenHelper.CreateTokenEntity(user, access, refresh);

                await _tokenRepository.CreateAsync(tokenEntity);

                response.Model = new LoginResponseDTO
                {
                    AccessToken = access.Token,
                    RefreshToken = refresh.Token,
                    ExpiresAt = access.ExpiresAt,
                    UserId = user.UserId,
                    Username = user.UserName
                };

                response.Message = "Autenticación exitosa.";
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = $"Error al autenticar: {ex.Message}";
            }

            return response;
        }

        #endregion
    }
}
