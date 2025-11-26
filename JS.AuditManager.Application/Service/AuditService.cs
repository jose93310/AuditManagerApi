using JS.AuditManager.Application.DTO.Audit;
using JS.AuditManager.Application.IService;
using JS.AuditManager.Domain.Enum;
using JS.AuditManager.Domain.Filters;
using JS.AuditManager.Domain.IRepository;
using JS.AuditManager.Domain.ModelEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.AuditManager.Application.Service
{
    public class AuditService : IAuditService
    {
        private readonly IAuditRepository _auditRepo;

        public AuditService(IAuditRepository auditRepo)
        {
            _auditRepo = auditRepo;
        }

        public async Task<SingleResponse<int>> CreateAuditAsync(AuditCreateDTO dto, Guid createdBy)
        {
            var audit = new Audit
            {
                Title = dto.Title,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                DepartmentId = dto.DepartmentId,
                ResponsibleId = dto.ResponsibleId,
                StatusId = (byte)AuditStatus.Pendiente,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = createdBy
            };

            await _auditRepo.AddAsync(audit);
            return new SingleResponse<int> { Model = audit.AuditId, Message="Auditoría creada satisfactoriamente" };
        }

        public async Task<List<Audit>> GetAuditsByFilterAsync(AuditFilterDTO dto)
        {
            var filter = new AuditFilter
            {
                FromDate = dto.FromDate,
                ToDate = dto.ToDate,
                StatusId = dto.StatusId
            };

            return await _auditRepo.GetByFilterAsync(filter);
        }

        public async Task<SingleResponse<bool>> UpdateAuditAsync(AuditUpdateDTO dto, Guid modifiedBy)
        {
            var audit = await _auditRepo.GetByIdAsync(dto.AuditId);
            if (audit == null || audit.StatusId != 1)
                return new SingleResponse<bool> { DidError = true, ErrorMessage = "Solo se puede actualizar auditorías en estado Pendiente." };

            audit.Title = dto.Title;
            audit.StartDate = dto.StartDate;
            audit.EndDate = dto.EndDate;
            audit.DepartmentId = dto.DepartmentId;
            audit.ResponsibleId = dto.ResponsibleId;
            audit.ModifiedAt = DateTime.UtcNow;
            audit.ModifiedBy = modifiedBy;

            await _auditRepo.UpdateAsync(audit);
            return new SingleResponse<bool> { Model = true, Message="Auditoría actualizada correctamente"};
        }

        public async Task<SingleResponse<bool>> ChangeAuditStatusAsync(AuditStatusChangeDTO dto, Guid modifiedBy)
        {
            var audit = await _auditRepo.GetByIdAsync(dto.AuditId);
            if (audit == null)
                return new SingleResponse<bool> { DidError = true, ErrorMessage = "Auditoría no encontrada." };

            audit.StatusId = dto.NewStatusId;
            audit.ModifiedAt = DateTime.UtcNow;
            audit.ModifiedBy = modifiedBy;

            await _auditRepo.UpdateAsync(audit);
            return new SingleResponse<bool> { Model = true, Message = "Estatus de Auditoría actualizado correctamente" };
        }
    }

}
