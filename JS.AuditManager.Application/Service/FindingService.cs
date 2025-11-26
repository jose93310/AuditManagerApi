using JS.AuditManager.Application.DTO.Finding;
using JS.AuditManager.Application.IService;
using JS.AuditManager.Domain.Enum;
using JS.AuditManager.Domain.IRepository;
using JS.AuditManager.Domain.ModelEntity;

namespace JS.AuditManager.Application.Service
{

    public class FindingService : IFindingService
    {
        private readonly IFindingRepository _findingRepo;
        private readonly IAuditRepository _auditRepo;

        public FindingService(IFindingRepository findingRepo, IAuditRepository auditRepo)
        {
            _findingRepo = findingRepo;
            _auditRepo = auditRepo;
        }

        public async Task<SingleResponse<int>> CreateFindingAsync(FindingCreateDTO dto, Guid createdBy)
        {
            // Validar existencia de auditoría
            if (!await _auditRepo.ExistsAsync(dto.AuditId))
            {
                return new SingleResponse<int>
                {
                    DidError = true,
                    ErrorMessage = "El ID de auditoría no existe.",
                    Message = "Validación fallida: AuditId inválido."
                };
            }

            // Validar TypeId y SeverityId contra enums
            if (!Enum.IsDefined(typeof(TypeFinding), dto.TypeId))
            {
                return new SingleResponse<int>
                {
                    DidError = true,
                    ErrorMessage = "Tipo de hallazgo inválido.",
                    Message = "Validación fallida: TypeId no valido."
                };
            }

            if (!Enum.IsDefined(typeof(SeverityLevel), dto.SeverityId))
            {
                return new SingleResponse<int>
                {
                    DidError = true,
                    ErrorMessage = "Nivel de severidad inválido.",
                    Message = "Validación fallida: SeverityId no valido."
                };
            }

            var finding = new Finding
            {
                Description = dto.Description,
                TypeId = dto.TypeId,
                SeverityId = dto.SeverityId,
                DetentionDate = dto.DetentionDate,
                AuditId = dto.AuditId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = createdBy
            };

            await _findingRepo.AddAsync(finding);

            return new SingleResponse<int>
            {
                Model = finding.FindingId,
                Message = "Hallazgo registrado correctamente."
            };
        }

        #region UpdateFindingAsync
        public async Task<SingleResponse<bool>> UpdateFindingAsync(FindingUpdateDTO dto, Guid modifiedBy)
        {
            var finding = await _findingRepo.GetByIdAsync(dto.FindingId);
            if (finding == null)
            {
                return new SingleResponse<bool>
                {
                    DidError = true,
                    ErrorMessage = "Hallazgo no encontrado.",
                    Message = "No se encontró el registro a actualizar.",
                    Model = false
                };
            }

            // Validar FK Audit
            if (!await _auditRepo.ExistsAsync(dto.AuditId))
            {
                return new SingleResponse<bool>
                {
                    DidError = true,
                    ErrorMessage = "El ID de auditoría no existe.",
                    Message = "Validación fallida: AuditId inválido.",
                    Model = false
                };
            }

            // Validar enums
            if (!Enum.IsDefined(typeof(TypeFinding), dto.TypeId))
            {
                return new SingleResponse<bool>
                {
                    DidError = true,
                    ErrorMessage = "Tipo de hallazgo inválido.",
                    Message = "Validación fallida: TypeId no valido.",
                    Model = false
                };
            }

            if (!Enum.IsDefined(typeof(SeverityLevel), dto.SeverityId))
            {
                return new SingleResponse<bool>
                {
                    DidError = true,
                    ErrorMessage = "Nivel de severidad inválido.",
                    Message = "Validación fallida: SeverityId no valido.",
                    Model = false
                };
            }

            finding.Description = dto.Description;
            finding.TypeId = dto.TypeId;
            finding.SeverityId = dto.SeverityId;
            finding.DetentionDate = dto.DetentionDate;
            finding.AuditId = dto.AuditId;
            finding.ModifiedAt = DateTime.UtcNow;
            finding.ModifiedBy = modifiedBy;

            await _findingRepo.UpdateAsync(finding);

            return new SingleResponse<bool>
            {
                Model = true,
                Message = "Hallazgo actualizado correctamente."
            };
        }
        #endregion

        #region GetFindingsAsync

        public async Task<SingleResponse<List<Finding>>> GetByFilterAsync(FindingFilterDTO filter)
        {
            if (!filter.AuditId.HasValue && !filter.SeverityId.HasValue)
            {
                return new SingleResponse<List<Finding>>
                {
                    DidError = true,
                    ErrorMessage = "Debe especificar al menos un criterio de filtro.",
                    Message = "Validación fallida: filtros vacíos.",
                    Model = new List<Finding>()
                };
            }

            var findings = await _findingRepo.GetByFilterAsync(new Domain.Filters.FindingFilter() { AuditId = filter.AuditId, SeverityId = filter.SeverityId });

            return new SingleResponse<List<Finding>>
            {
                Model = findings,
                Message = findings.Any()
                    ? "Hallazgos consultados correctamente."
                    : "No se encontraron hallazgos con los filtros especificados."
            };
        }

        #endregion

        #region DeleteFindingAsync
        /// <summary>
        /// Metodo para eliminar hallazgos (Finding), solo si la auditoría está “En Proceso”
        /// </summary>
        /// <param name="findingId">Id del hallazgo a eliminar</param>
        /// <param name="deletedBy">Id del usuario que realiza la accion</param>
        /// <returns>Retorna boolean con true si la accion se ejecuta correctamente</returns>
        public async Task<SingleResponse<bool>> DeleteFindingAsync(int findingId, Guid deletedBy)
        {
            var finding = await _findingRepo.GetByIdAsync(findingId);
            if (finding == null)
            {
                return new SingleResponse<bool>
                {
                    DidError = true,
                    ErrorMessage = "Hallazgo no encontrado.",
                    Message = "No se encontró el registro a eliminar.",
                    Model = false
                };
            }

            // Validar estado de la auditoría
            var audit = await _auditRepo.GetByIdAsync(finding.AuditId);
            if (audit == null)
            {
                return new SingleResponse<bool>
                {
                    DidError = true,
                    ErrorMessage = "La auditoría asociada no existe.",
                    Message = "Validación fallida: AuditId inválido.",
                    Model = false
                };
            }

            if (audit.StatusId != (byte)AuditStatus.EnProceso)
            {
                return new SingleResponse<bool>
                {
                    DidError = true,
                    ErrorMessage = "Solo se pueden eliminar hallazgos si la auditoría está en proceso.",
                    Message = "Operación no permitida: estatus de auditoría inválido.",
                    Model = false
                };
            }

            await _findingRepo.DeleteAsync(finding);

            return new SingleResponse<bool>
            {
                Model = true,
                Message = "Hallazgo eliminado correctamente."
            };
        }
        #endregion
    }
}
