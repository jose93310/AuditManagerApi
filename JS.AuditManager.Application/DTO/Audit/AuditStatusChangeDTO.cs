using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.AuditManager.Application.DTO.Audit
{
    public class AuditStatusChangeDTO
    {
        [Required]
        public int AuditId { get; set; }

        [Required]
        public byte NewStatusId { get; set; } // 1 = Pendiente, 2 = En Proceso, 3 = Finalizada
    }

}
