using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.AuditManager.Application.DTO.Audit
{
    public class AuditUpdateDTO
    {
        [Required]
        public int AuditId { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public int ResponsibleId { get; set; }
    }

}
