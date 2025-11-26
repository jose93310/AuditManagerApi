using JS.AuditManager.Domain.IRepository;
using JS.AuditManager.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.AuditManager.Infrastructure.Responsible
{
    public class ResponsibleRepository : IResponsibleRepository
    {
        private readonly AuditContext _context;

        public ResponsibleRepository(AuditContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Domain.ModelEntity.Responsible responsible)
        {
            _context.Responsibles.Add(responsible);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int responsibleId)
        {
            return await _context.Responsibles.AnyAsync(r => r.ResponsibleId == responsibleId);
        }

        public async Task<List<Domain.ModelEntity.Audit>> GetByResponsibleAsync(int responsibleId)
        {
            return await _context.Audits
                .Where(a => a.ResponsibleId == responsibleId)
                .ToListAsync();
        }
    }


}
