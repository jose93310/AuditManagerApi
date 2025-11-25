using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.AuditManager.Domain.Enum
{
    public enum AuditStatus : byte
    {
        Pendiente = 1,
        EnProceso = 2,
        Finalizada = 3
    }

}
