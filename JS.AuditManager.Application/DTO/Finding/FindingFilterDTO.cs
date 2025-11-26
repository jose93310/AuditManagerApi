using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.AuditManager.Application.DTO.Finding
{
    public class FindingFilterDTO
    {
        public int? AuditId { get; set; }
        public byte? SeverityId { get; set; }
    }

}
