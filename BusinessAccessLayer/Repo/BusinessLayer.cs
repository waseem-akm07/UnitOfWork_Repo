using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using DataTransferObject.DTO;
using DataAccessLayer.Model;

namespace BusinessAccessLayer.Repo
{
    public class BusinessLayer : IBusinessLayer
    {
        private Practice2Entities _entities;

        /// <summary>
        /// Initialize DbContext Enitities
        /// </summary>
        /// <param name="entities"> Get table from DbContext </param>
        public BusinessLayer(Practice2Entities entities)
        {
            _entities = entities;
        }


        /// <summary>
        /// Get Employees Data List
        /// </summary>
        /// <returns> Employees List </returns>
        public IEnumerable<EmployeeGetModel> GetEmployees()
        {
            IEnumerable<EmployeeGetModel> data = new List<EmployeeGetModel>();

            data = _entities.Employees
                .Include(x => x.Company)
                .Include(x => x.Department)
                .Include(x => x.EmployeeCourses.Select(y => y.Course))
                .AsEnumerable()
                .Select(x => new EmployeeGetModel
                {
                    EmployeeId = x.EmployeeId,
                    EmployeeName = x.EmployeeName,
                    EmployeeAddress = x.EmployeeAddress,
                    Company = new CompanyModel
                    {
                        CompanyId = x.Company.CompanyId,
                        CompanyName = x.Company.CompanyName
                    },
                    Department = new DepartmentModel
                    {
                        DepartmentId = x.Department.DepartmentId,
                        DepartmentName = x.Department.DepartmentName
                    },
                    Course = parseStudentSubjects(x.EmployeeCourses.ToList())
                }).ToList();

            return data;
        }
        
        /// <summary>
        /// Get specific employee data
        /// </summary>
        /// <param name="id"> employee id </param>
        /// <returns> specific employee data </returns>
        public IEnumerable<EmployeeGetModel> GetEmployee(string id)
        {
            IEnumerable<EmployeeGetModel> data = new List<EmployeeGetModel>();

            data = _entities.Employees
                .Include(x => x.Company)
                .Include(x => x.Department)
                .Include(x => x.EmployeeCourses.Select(y => y.Course))
                .AsEnumerable()
                .Where(x => x.EmployeeId == id)
                .Select(x => new EmployeeGetModel
                {
                    EmployeeId = x.EmployeeId,
                    EmployeeName = x.EmployeeName,
                    EmployeeAddress = x.EmployeeAddress,
                    Company = new CompanyModel
                    {
                        CompanyId = x.Company.CompanyId,
                        CompanyName = x.Company.CompanyName
                    },
                    Department = new DepartmentModel
                    {
                        DepartmentId = x.Department.DepartmentId,
                        DepartmentName = x.Department.DepartmentName
                    },
                    Course = parseStudentSubjects(x.EmployeeCourses.ToList())
                }).ToList();

            return data;
        }

        private List<CourseModel> parseStudentSubjects(List<EmployeeCourse> courses)
        {
            List<CourseModel> _courses = new List<CourseModel>();
            foreach (var sub in courses)
            {
                if(sub.CourseId != null)
                {
                    CourseModel subject = new CourseModel()
                    {
                        CourseId = sub.Course.CourseId,
                        CourseName = sub.Course.CourseName
                    };
                    _courses.Add(subject);
                }
            }
            return _courses;
        }

        /// <summary>
        /// Register new employee record
        /// </summary>
        /// <param name="model"> fetch employee data for register </param>
        public void Create(EmployeeInsertModel model)
        {

            #region for add new Company
            Company company = new Company();
            // filtering company allready exist or not
            var CompanyData = _entities.Companies.Where(x => x.CompanyName == model.Company.CompanyName).FirstOrDefault();
            var CompanyId = "";
            //if company is not exist in database then add new company
            if (CompanyData == null)
            {
                Guid CompanyGuid = Guid.NewGuid();
                CompanyId = CompanyGuid.ToString();
                company.CompanyId = CompanyId;
                company.CompanyName = model.Company.CompanyName;
                _entities.Companies.Add(company);
            }
            else
                CompanyId = CompanyData.CompanyId;
            #endregion

            #region for add new Department
            Department department = new Department();
            // filtering department allready exist or not
            var DepartmentData = _entities.Departments.Where(x => x.DepartmentName == model.Department.DepartmentName).FirstOrDefault();
            var DepartmentId = "";
            //if department is not exist in database then add new department
            if (DepartmentData == null)
            {
                Guid DepartmentGuid = Guid.NewGuid();
                DepartmentId = DepartmentGuid.ToString();
                department.DepartmentId = DepartmentId;
                department.DepartmentName = model.Department.DepartmentName;
                _entities.Departments.Add(department);
            }
            else
                DepartmentId = DepartmentData.DepartmentId;
            #endregion


            #region for add new Employee
            Employee employee = new Employee();
            Guid EmployeeGuid = Guid.NewGuid();
            var EmployeeId = EmployeeGuid.ToString();
            employee.EmployeeId = EmployeeId;
            employee.EmployeeName = model.EmployeeName;
            employee.EmployeeAddress = model.EmployeeAddress;
            employee.CompanyId = CompanyId;
            employee.DepartmentId = DepartmentId;
            _entities.Employees.Add(employee);
            #endregion

            #region for add new Course
            Course course = new Course();
            // filtering course allready exist or not
            var CourseData = _entities.Courses.Where(x => x.CourseName == model.Course.CourseName).FirstOrDefault();
            var CourseId = "";
            //if course is not exist in database then add new course
            if (CourseData == null)
            {
                Guid CourseGuid = Guid.NewGuid();
                CourseId = CourseGuid.ToString();
                course.CourseId = CourseId;
                course.CourseName = model.Course.CourseName;
                _entities.Courses.Add(course);
            }
            else
                CompanyId = CourseData.CourseId;
            #endregion

            #region for mapping subject to employee
            EmployeeCourse employeeCourse = new EmployeeCourse();
            Guid EmployeeCourseGuid = Guid.NewGuid();
            employeeCourse.EmployeeCourseId = EmployeeCourseGuid.ToString();
            employeeCourse.EmployeeId = EmployeeId;
            employeeCourse.CourseId = CourseId;
            _entities.EmployeeCourses.Add(employeeCourse);
            #endregion
        }

        /// <summary>
        /// Update specific employee data
        /// </summary>
        /// <param name="id"> Employee id </param>
        /// <param name="model"> fetch employee data for update </param>
        public void Update(string id, EmployeeInsertModel model)
        {
            #region for update Employee, Company, Department
            var EmployeeData = _entities.Employees.Where(x => x.EmployeeId == id).FirstOrDefault();
            EmployeeData.EmployeeName = model.EmployeeName;
            EmployeeData.EmployeeAddress = model.EmployeeAddress;
            EmployeeData.Company.CompanyName = model.Company.CompanyName;
            EmployeeData.Department.DepartmentName = model.Department.DepartmentName;
            _entities.Entry(EmployeeData).State = EntityState.Modified;
            #endregion

            #region for update EmployeeCourse
            //find employee course id
            var EmployeeCourse = _entities.EmployeeCourses.Where(x => x.EmployeeId == id).FirstOrDefault();
            //find assing course list of employee
            var Course = _entities.Courses.Where(x => x.CourseId == EmployeeCourse.CourseId).FirstOrDefault();
            Course.CourseName = model.Course.CourseName;
            _entities.Entry(Course).State = EntityState.Modified;
            #endregion
        }

        /// <summary>
        /// Delete employee from list
        /// </summary>
        /// <param name="id"> employee id </param>
        public void Delete(string id)
        {
            var Employee = _entities.Employees.Where(x => x.EmployeeId == id).FirstOrDefault();
            _entities.Entry(Employee).State = EntityState.Deleted;
        }
    }
}
