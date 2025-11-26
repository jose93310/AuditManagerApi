using Asp.Versioning;
using JS.AuditManager.Application.DTO.Responsible;
using JS.AuditManager.Application.Helper.Security;
using JS.AuditManager.Application.IService;
using JS.AuditManager.Domain.ModelEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JS.AuditManager.RestApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/responsibles")]
    [Authorize]
    public class ResponsibleController : ControllerBase
    {
        private readonly IResponsibleService _service;

        public ResponsibleController(IResponsibleService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ResponsibleCreateDTO dto)
        {
            var userId = TokenHelper.GetUserId(User);
            if (userId == null)
                return Unauthorized(new SingleResponse<bool> { DidError = true, ErrorMessage = "Usuario no autenticado.", Message = "Token inválido o ausente.", Model = false });

            var result = await _service.CreateResponsibleAsync(dto, userId.Value);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpGet("{responsibleId}/audits")]
        public async Task<IActionResult> GetAuditsByResponsible(int responsibleId)
        {
            var result = await _service.GetAuditsByResponsibleAsync(responsibleId);
            return Ok(result);
        }
    }

}
