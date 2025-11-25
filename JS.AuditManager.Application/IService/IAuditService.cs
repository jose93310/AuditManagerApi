using JS.AuditManager.Application.DTO.Audit;
using JS.AuditManager.Domain.ModelEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.AuditManager.Application.IService
{
    public interface IAuditService
    {
        Task<SingleResponse<int>> CreateAuditAsync(AuditCreateDTO dto, Guid createdBy);
        Task<List<AuditEntity>> GetAuditsByFilterAsync(AuditFilterDTO filter);
        Task<SingleResponse<bool>> UpdateAuditAsync(AuditUpdateDTO dto, Guid modifiedBy);
        Task<SingleResponse<bool>> ChangeAuditStatusAsync(AuditStatusChangeDTO dto, Guid modifiedBy);
    }

}
