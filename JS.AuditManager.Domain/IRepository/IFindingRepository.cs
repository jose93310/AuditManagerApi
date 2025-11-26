using JS.AuditManager.Domain.Filters;
using JS.AuditManager.Domain.ModelEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.AuditManager.Domain.IRepository
{
    public interface IFindingRepository
    {
        Task AddAsync(Finding finding);
        Task<Finding?> GetByIdAsync(int findingId);
        Task UpdateAsync(Finding finding);
        Task<List<Finding>> GetByFilterAsync(FindingFilter findingFilter);
        Task DeleteAsync(Finding finding);

    }

}
