
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTO
{
    public class DepartmentModel
    {
        public string DepartmentId { get; set; }
        [Required]
        public string DepartmentName { get; set; }
    }
}
