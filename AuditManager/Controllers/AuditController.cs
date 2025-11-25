using Asp.Versioning;
using JS.AuditManager.Application.DTO.Audit;
using JS.AuditManager.Application.IService;
using JS.AuditManager.Application.Helper.Security;
using JS.AuditManager.Domain.ModelEntity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace JS.AuditManager.RestApi.Controllers
{
    /// <summary>
    /// Controlador para gestionar auditorías.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/audits")]
    [Authorize]
    public class AuditController : ControllerBase
    {
        private readonly IAuditService _auditService;

        public AuditController(IAuditService auditService)
        {
            _auditService = auditService;
        }

        #region CreateAudit
        /// <summary>
        /// Crea una nueva auditoría.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateAudit([FromBody] AuditCreateDTO dto)
        {
            var createdBy = TokenHelper.GetUserId(User);
            if (createdBy == null)
                return Unauthorized(new SingleResponse<bool> { DidError = true, ErrorMessage = "Usuario no autenticado." });

            var result = await _auditService.CreateAuditAsync(dto, createdBy.Value);
            return StatusCode(StatusCodes.Status201Created, result);
        }
        #endregion

        #region GetAuditsByFilter
        /// <summary>
        /// Consulta auditorías por rango de fechas y estado.
        /// </summary>
        [HttpPost("filter")]
        public async Task<IActionResult> GetAuditsByFilter([FromBody] AuditFilterDTO filter)
        {
            var audits = await _auditService.GetAuditsByFilterAsync(filter);
            return Ok(audits);
        }
        #endregion

        #region UpdateAudit
        /// <summary>
        /// Actualiza una auditoría (solo si está en estado Pendiente).
        /// </summary>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateAudit([FromBody] AuditUpdateDTO dto)
        {
            var modifiedBy = TokenHelper.GetUserId(User);
            if (modifiedBy == null)
                return Unauthorized(new SingleResponse<bool> { DidError = true, ErrorMessage = "Usuario no autenticado." });

            var result = await _auditService.UpdateAuditAsync(dto, modifiedBy.Value);
            return Ok(result);
        }
        #endregion

        #region ChangeAuditStatus
        /// <summary>
        /// Cambia el estado de una auditoría.
        /// </summary>
        [HttpPut("status")]
        public async Task<IActionResult> UpdateAuditStatus([FromBody] AuditStatusChangeDTO dto)
        {
            var modifiedBy = TokenHelper.GetUserId(User);
            if (modifiedBy == null)
                return Unauthorized(new SingleResponse<bool> { DidError = true, ErrorMessage = "Usuario no autenticado." });

            var result = await _auditService.ChangeAuditStatusAsync(dto, modifiedBy.Value);
            return Ok(result);
        }
        #endregion
    }
}

