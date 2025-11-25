using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.AuditManager.Application.DTO.User
{
    public class UserRegisterResponseDTO
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = string.Empty;
    }
}
