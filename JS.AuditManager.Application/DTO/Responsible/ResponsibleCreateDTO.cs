using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.AuditManager.Application.DTO.Responsible
{
    public class ResponsibleCreateDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int IdentificationNumber { get; set; }

        [Required]
        public byte IdentificationTypeId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public int DepartmentId { get; set; }
    }

}
