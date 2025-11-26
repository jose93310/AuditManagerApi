using Asp.Versioning;
using JS.AuditManager.Application.DTO.Finding;
using JS.AuditManager.Application.Helper.Security;
using JS.AuditManager.Application.IService;
using JS.AuditManager.Domain.ModelEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JS.AuditManager.RestApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/findings")]
    [Authorize]
    public class FindingController : ControllerBase
    {
        private readonly IFindingService _service;

        public FindingController(IFindingService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFinding([FromBody] FindingCreateDTO dto)
        {
            var userId = TokenHelper.GetUserId(User);
            if (userId == null)
                return Unauthorized(new SingleResponse<bool> { DidError = true, ErrorMessage = "Usuario no autenticado." });

            var result = await _service.CreateFindingAsync(dto, userId.Value);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateFinding([FromBody] FindingUpdateDTO dto)
        {
            var userId = TokenHelper.GetUserId(User);
            if (userId == null)
                return Unauthorized(new SingleResponse<bool> { DidError = true, ErrorMessage = "Usuario no autenticado." });

            var result = await _service.UpdateFindingAsync(dto, userId.Value);
            return Ok(result);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFindingByFilters([FromQuery] int? auditId, [FromQuery] byte? severityId)
        {
            var filter = new FindingFilterDTO
            {
                AuditId = auditId,
                SeverityId = severityId
            };

            var result = await _service.GetByFilterAsync(filter);
            return result.DidError ? BadRequest(result) : Ok(result);
        }


        [HttpDelete("{findingId}")]
        public async Task<IActionResult> DeleteFinding(int findingId)
        {
            var userId = TokenHelper.GetUserId(User);
            if (userId == null)
                return Unauthorized(new SingleResponse<bool> { DidError = true, ErrorMessage = "Usuario no autenticado.", Message = "Token inválido o ausente.", Model = false });

            var result = await _service.DeleteFindingAsync(findingId, userId.Value);
            return result.DidError ? BadRequest(result) : Ok(result);
        }

    }

}
