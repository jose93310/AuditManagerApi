using JS.AuditManager.Application.DTO.Auth;
using JS.AuditManager.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.AuditManager.Application.IService
{
    public interface IAuthService
    {
        Task<ISingleResponse<LoginResponseDTO>> LoginAsync(LoginRequestDTO request);
    }
}
