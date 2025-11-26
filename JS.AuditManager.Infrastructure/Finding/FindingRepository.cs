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

namespace JS.AuditManager.Infrastructure.Finding
{
    public class FindingRepository : IFindingRepository
    {
        private readonly AuditContext _context;

        public FindingRepository(AuditContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Domain.ModelEntity.Finding finding)
        {
            _context.Findings.Add(finding);
            await _context.SaveChangesAsync();
        }

        public async Task<Domain.ModelEntity.Finding?> GetByIdAsync(int findingId)
        {
            return await _context.Findings.FindAsync(findingId);
        }

        public async Task UpdateAsync(Domain.ModelEntity.Finding finding)
        {
            _context.Findings.Update(finding);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Domain.ModelEntity.Finding>> GetByFilterAsync(FindingFilter filter)
        {
            var query = _context.Findings.AsQueryable();

            if (filter.AuditId.HasValue)
                query = query.Where(f => f.AuditId == filter.AuditId.Value);

            if (filter.SeverityId.HasValue)
                query = query.Where(f => f.SeverityId == filter.SeverityId.Value);

            return await query.ToListAsync();
        }


        public async Task DeleteAsync(Domain.ModelEntity.Finding finding)
        {
            _context.Findings.Remove(finding);
            await _context.SaveChangesAsync();
        }


    }

}
