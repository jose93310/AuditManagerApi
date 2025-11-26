using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.AuditManager.Domain.Filters
{
    public class FindingFilter
    {
        public int? AuditId { get; set; }
        public byte? SeverityId { get; set; }
    }
}
