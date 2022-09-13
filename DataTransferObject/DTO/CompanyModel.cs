using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTO
{

    public class CompanyModel
    {
        public string CompanyId { get; set; }
        [Required]
        public string CompanyName { get; set; }
    }
}
