using SkillsLab_OOP.DAL.Common;
using SkillsLab_OOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillsLab_OOP.DAL
{
    public interface IEnrollmentDAL
    {
        IEnumerable<EnrollmentModel> GetAll();
        EnrollmentModel GetById(int EnrollmentId);
        bool Add(EnrollmentModel model);
        EnrollmentModel Update(EnrollmentModel model);
        bool Delete(int EnrollmentId);
    }
    public class EnrollmentDAL : IEnrollmentDAL
    {
        public IEnumerable<EnrollmentModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public EnrollmentModel GetById(int EnrollmentId)
        {
            throw new NotImplementedException();
        }

        public bool Add(EnrollmentModel model)
        {
            throw new NotImplementedException();
        }

        public EnrollmentModel Update(EnrollmentModel model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int EnrollmentId)
        {
            throw new NotImplementedException();
        }

    }
}
