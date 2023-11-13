using SkillsLab_OOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillsLab_OOP.DAL
{
    public interface IEmployeeDAL
    {
        IEnumerable<EmployeeModel> GetAll();
        EmployeeModel GetById(int employeeId);
        EmployeeModel Update(EmployeeModel model);
        bool Delete(int employeeId);
    }
    internal class EmployeeDAL : IEmployeeDAL
    {
        public IEnumerable<EmployeeModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public EmployeeModel GetById(int employeeId)
        {
            throw new NotImplementedException();
        }

        public EmployeeModel Update(EmployeeModel model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int employeeId)
        {
            throw new NotImplementedException();
        }
    }
}
