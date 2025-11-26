using JS.AuditManager.Application.DTO.Responsible;
using JS.AuditManager.Domain.ModelEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.AuditManager.Application.IService
{
    public interface IResponsibleService
    {
        Task<SingleResponse<int>> CreateResponsibleAsync(ResponsibleCreateDTO dto, Guid createdBy);
          Task<ListResponse<Audit>> GetAuditsByResponsibleAsync(int responsibleId);
    }

}
