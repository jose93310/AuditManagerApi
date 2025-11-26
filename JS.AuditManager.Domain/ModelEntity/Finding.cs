using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.AuditManager.Domain.ModelEntity
{
    public class Finding
    {
        public int FindingId { get; set; }
        public string Description { get; set; } = string.Empty;
        public byte TypeId { get; set; }
        public byte SeverityId { get; set; }
        public DateTime DetentionDate { get; set; }
        public int AuditId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public Guid? ModifiedBy { get; set; }

        public Audit? Audit { get; set; }

        //public SeverityLevel? Severity { get; set; }
        //public TypeFinding? Type { get; set; }
    }

}
