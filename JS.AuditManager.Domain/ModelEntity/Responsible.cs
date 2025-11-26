using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.AuditManager.Domain.ModelEntity
{
    public class Responsible
    {
        public int ResponsibleId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int IdentificationNumber { get; set; }
        public byte IdentificationTypeId { get; set; }
        public string Email { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public Guid? ModifiedBy { get; set; }

        //public Department? Department { get; set; }
    }

}
