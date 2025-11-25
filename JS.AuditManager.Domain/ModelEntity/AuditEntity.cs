using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.AuditManager.Domain.ModelEntity
{
    public class AuditEntity
    {
        public int AuditId { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int DepartmentId { get; set; }
        public int ResponsibleId { get; set; }
        public byte StatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public Guid? ModifiedBy { get; set; }

        // Relaciones
        //public Department? Department { get; set; }
        //public Responsible? Responsible { get; set; }
        //public Status? Status { get; set; }
    }

}
