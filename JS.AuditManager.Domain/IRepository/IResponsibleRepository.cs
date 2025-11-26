using JS.AuditManager.Domain.ModelEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.AuditManager.Domain.IRepository
{
    public interface IResponsibleRepository
    {
        Task AddAsync(Responsible responsible);
        Task<bool> ExistsAsync(int responsibleId); 
    }
}
