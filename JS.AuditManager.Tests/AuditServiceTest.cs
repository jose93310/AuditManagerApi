    using Xunit;
    using Moq;
    using FluentAssertions;
    using JS.AuditManager.Application.Service;
    using JS.AuditManager.Domain.IRepository;
    using JS.AuditManager.Application.DTO.Audit;
    using JS.AuditManager.Domain.ModelEntity;
    using JS.AuditManager.Domain.Enum;

namespace JS.AuditManager.Tests
{
    public class AuditServiceTests
    {
        private readonly Mock<IAuditRepository> _auditRepoMock;
        private readonly Mock<IResponsibleRepository> _responsibleRepoMock;
        private readonly AuditService _service;

        public AuditServiceTests()
        {
            _auditRepoMock = new Mock<IAuditRepository>();
            _responsibleRepoMock = new Mock<IResponsibleRepository>();
            _service = new AuditService(_auditRepoMock.Object, _responsibleRepoMock.Object);
        }

        #region CreateAudit
        /// <summary>
        /// Verifica que al crear una auditoría con datos válidos,
        /// el servicio retorne éxito y el mensaje esperado.
        /// </summary>
        [Fact]
        public async Task CreateAudit_ShouldReturnSuccess_WhenValidDto()
        {
            // Arrange
            var dto = new AuditCreateDTO
            {
                Title = "Test Audit",
                DepartmentId = 1,
                ResponsibleId = 5,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(1)
            };
            _auditRepoMock.Setup(r => r.AddAsync(It.IsAny<Audit>()))
                          .Returns(Task.CompletedTask);

            // Act
            var result = await _service.CreateAuditAsync(dto, Guid.NewGuid());

            // Assert
            result.DidError.Should().BeFalse();
            result.Message.Should().Be("Auditoría creada satisfactoriamente");
        }
        #endregion

        #region UpdateAudit
        /// <summary>
        /// Verifica que al intentar actualizar una auditoría inexistente,
        /// el servicio retorne error con el mensaje correspondiente.
        /// </summary>
        [Fact]
        public async Task UpdateAudit_ShouldFail_WhenAuditNotFound()
        {
            // Arrange
            _auditRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                          .ReturnsAsync((Audit)null);

            var dto = new AuditUpdateDTO { AuditId = 1, ResponsibleId = 5 };

            // Act

        }
        #endregion
    }
}
