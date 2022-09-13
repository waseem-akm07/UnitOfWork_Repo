using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject.DTO;

namespace BusinessAccessLayer.Repo
{
    public interface IBusinessLayer
    {
        IEnumerable<EmployeeGetModel> GetEmployees();
        IEnumerable<EmployeeGetModel> GetEmployee(string id);
        void Create(EmployeeInsertModel model);
        void Update(string id, EmployeeInsertModel model);
        void Delete(string id);
    }
}
