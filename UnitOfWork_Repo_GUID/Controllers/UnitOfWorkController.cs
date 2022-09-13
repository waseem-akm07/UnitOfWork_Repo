using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UnitOfWorkLayer.UOW;
using DataTransferObject.DTO;


namespace UnitOfWork_Repo_GUID.Controllers
{
    public class UnitOfWorkController : ApiController
    {
        private IUnitOfWork _unitOfWork = new UnitOfWork();

        /// <summary>
        /// Get Employees Data List
        /// </summary>
        /// <returns> Employees List </returns>
        [HttpGet]
        [Route("api/unitofwork/getemployees")]
        public IEnumerable<EmployeeGetModel> GetEmployees()
        {
            return _unitOfWork.BusinessLayer.GetEmployees();
        }


        /// <summary>
        /// Get specific employee data
        /// </summary>
        /// <param name="id"> employee id </param>
        /// <returns> specific employee data </returns>
        [HttpGet]
        [Route("api/unitofwork/getemployee/{id}")]
        public IEnumerable<EmployeeGetModel> GetEmployee(string id)
        {
            IEnumerable<EmployeeGetModel> data = new List<EmployeeGetModel>();

            if (id != null){ data = _unitOfWork.BusinessLayer.GetEmployee(id); }

            return data;
        }


        /// <summary>
        /// Register new employee record
        /// </summary>
        /// <param name="model"> fetch employee data for register </param>
        [HttpPost]
        [Route("api/unitofwork/postemployee")]
        public void PostEmployee(EmployeeInsertModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.BusinessLayer.Create(model);
                    _unitOfWork.Commit();
                }
                catch (Exception ex) { throw ex; }
            }
        }


        /// <summary>
        /// Update specific employee data
        /// </summary>
        /// <param name="id"> Employee id </param>
        /// <param name="model"> fetch employee data for update </param>
        [HttpPut]
        [Route("api/unitofwork/putemployee/{id}")]
        public void PutEmployee(string id, EmployeeInsertModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.BusinessLayer.Update(id, model);
                    _unitOfWork.Commit();
                }
                catch (Exception ex) { throw ex; }
            }
        }


        /// <summary>
        /// Delete employee from list
        /// </summary>
        /// <param name="id"> employee id </param>
        [HttpDelete]
        [Route("api/unitofwork/delete/{id}")]
        public void Delete(string id)
        {
            try
            {
                _unitOfWork.BusinessLayer.Delete(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
