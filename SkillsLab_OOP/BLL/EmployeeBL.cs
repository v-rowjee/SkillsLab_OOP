using SkillsLab_OOP.Models;
using SkillsLab_OOP.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillsLab_OOP.BL
{
    public interface IEmployeeBL
    {
        IEnumerable<EmployeeModel> GetAllEmployees();
        EmployeeModel GetEmployeeById(int employeeId);
        bool UpdateEmployee(EmployeeModel employee);
        bool DeleteEmployee(int employeeId);
    }
    public class EmployeeBL : IEmployeeBL
    {
        public readonly IEmployeeDAL _employeeDAL;

        public EmployeeBL(IEmployeeDAL employeeDAL)
        {
            _employeeDAL = employeeDAL;
        }
        public EmployeeBL()
        {
            _employeeDAL = new EmployeeDAL();
        }

        public EmployeeModel GetEmployeeById(int employeeId) { throw new NotImplementedException(); }
        public IEnumerable<EmployeeModel> GetAllEmployees() { throw new NotImplementedException(); }
        public bool UpdateEmployee(EmployeeModel employee) {  throw new NotImplementedException(); }
        public bool DeleteEmployee(int employeeId) { throw new NotImplementedException(); }
    }
}
