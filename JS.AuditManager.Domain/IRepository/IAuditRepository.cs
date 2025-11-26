using JS.AuditManager.Domain.Filters;
using JS.AuditManager.Domain.ModelEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.AuditManager.Domain.IRepository
{
    public interface IAuditRepository
    {
        Task AddAsync(Audit audit);
        Task<Audit?> GetByIdAsync(int auditId);
        Task<List<Audit>> GetByFilterAsync(AuditFilter filter);
        Task UpdateAsync(Audit audit);
        Task<bool> ExistsAsync(int auditId);
        Task<List<Audit>> GetByResponsibleAsync(int responsibleId);

    }
}
