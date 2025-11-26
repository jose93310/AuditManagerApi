using JS.AuditManager.Domain.Filters;
using JS.AuditManager.Domain.IRepository;
using JS.AuditManager.Domain.ModelEntity;
using JS.AuditManager.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.AuditManager.Infrastructure.Audit
{
    public class AuditRepository : IAuditRepository
    {
        private readonly AuditContext _context;

        public AuditRepository(AuditContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Domain.ModelEntity.Audit audit)
        {
            _context.Audits.Add(audit);
            await _context.SaveChangesAsync();
        }

        public async Task<Domain.ModelEntity.Audit?> GetByIdAsync(int auditId)
        {
            return await _context.Audits.FindAsync(auditId);
        }

        public async Task<List<Domain.ModelEntity.Audit>> GetByFilterAsync(AuditFilter filter)
        {
            var query = _context.Audits.AsQueryable();

            if (filter.FromDate.HasValue)
                query = query.Where(a => a.StartDate >= filter.FromDate.Value);

            if (filter.ToDate.HasValue)
                query = query.Where(a => a.EndDate <= filter.ToDate.Value);

            if (filter.StatusId.HasValue)
                query = query.Where(a => a.StatusId == filter.StatusId.Value);

            return await query.ToListAsync();
        }

        public async Task UpdateAsync(Domain.ModelEntity.Audit audit)
        {
            _context.Audits.Update(audit);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int auditId)
        {
            return await _context.Audits.AnyAsync(a => a.AuditId == auditId);
        }

    }

}
