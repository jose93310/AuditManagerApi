using JS.AuditManager.Application.DTO.Finding;
using JS.AuditManager.Domain.ModelEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.AuditManager.Application.IService
{
    public interface IFindingService
    {
        Task<SingleResponse<int>> CreateFindingAsync(FindingCreateDTO dto, Guid createdBy);
        Task<SingleResponse<bool>> UpdateFindingAsync(FindingUpdateDTO dto, Guid modifiedBy);
        Task<SingleResponse<List<Finding>>> GetByFilterAsync(FindingFilterDTO findingFilter);
        Task<SingleResponse<bool>> DeleteFindingAsync(int findingId, Guid deletedBy);


    }

}
