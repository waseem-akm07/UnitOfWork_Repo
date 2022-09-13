using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTO
{
    public class CourseModel
    {
        public string CourseId { get; set; }
        [Required]
        public string CourseName { get; set; }
    }
}
