using JS.AuditManager.Application.DTO.Responsible;
using JS.AuditManager.Application.IService;
using JS.AuditManager.Domain.IRepository;
using JS.AuditManager.Domain.ModelEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.AuditManager.Application.Service
{
    public class ResponsibleService : IResponsibleService
    {
        private readonly IResponsibleRepository _responRepo;
        private readonly IAuditRepository _auditRepo;

        public ResponsibleService(IResponsibleRepository repo, IAuditRepository auditRepo)
        {
            _responRepo = repo;
            _auditRepo = auditRepo;
        }

        public async Task<SingleResponse<int>> CreateResponsibleAsync(ResponsibleCreateDTO dto, Guid createdBy)
        {
            var responsible = new Responsible
            {
                Name = dto.Name,
                IdentificationNumber = dto.IdentificationNumber,
                IdentificationTypeId = dto.IdentificationTypeId,
                Email = dto.Email,
                DepartmentId = dto.DepartmentId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = createdBy
            };

            await _responRepo.AddAsync(responsible);

            return new SingleResponse<int>
            {
                Model = responsible.ResponsibleId,
                Message = "Responsable registrado correctamente."
            };
        }

        
        public async Task<ListResponse<Audit>> GetAuditsByResponsibleAsync(int responsibleId)
        {
            var audits = await _auditRepo.GetByResponsibleAsync(responsibleId);

            return new ListResponse<Audit>
            {
                Model = audits,
                Message = audits.Any()
                    ? "Auditorías consultadas correctamente."
                    : "No se encontraron auditorías para el responsable."
            };
        }
    }

}
