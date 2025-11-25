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
        Task AddAsync(AuditEntity audit);
        Task<AuditEntity?> GetByIdAsync(int auditId);
        Task<List<AuditEntity>> GetByFilterAsync(AuditFilter filter);
        Task UpdateAsync(AuditEntity audit);
    }
}
