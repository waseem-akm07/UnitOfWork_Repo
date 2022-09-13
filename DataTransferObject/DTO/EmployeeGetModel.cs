using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTO
{
    public class EmployeeGetModel
    {
        public string EmployeeId { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        [Required]
        public string EmployeeAddress { get; set; }
        [Required]
        public CompanyModel Company { get; set; }
        public DepartmentModel Department { get; set; }
        public List<CourseModel> Course { get; set; }
    }
}
