using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.AuditManager.Application.DTO.Finding
{
    public class FindingCreateDTO
    {
        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public byte TypeId { get; set; }

        [Required]
        public byte SeverityId { get; set; }

        [Required]
        public DateTime DetentionDate { get; set; }

        [Required]
        public int AuditId { get; set; }
    }

}
