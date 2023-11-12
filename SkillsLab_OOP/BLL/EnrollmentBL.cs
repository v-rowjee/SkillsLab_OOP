using SkillsLab_OOP.Models;
using SkillsLab_OOP.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillsLab_OOP.DAL.Common;

namespace SkillsLab_OOP.BL
{
    public interface IEnrollmentBL
    {
        IEnumerable<EnrollmentModel> GetAllEnrollments();
        EnrollmentModel GetEnrollmentById(int EnrollmentId);
        bool AddEnrollment(EnrollmentModel model);
        bool UpdateEnrollment(EnrollmentModel model);
        bool DeleteEnrollment(int EnrollmentId);
    }
    public class EnrollmentBL : IEnrollmentBL
    {
        private readonly IDAL<EnrollmentModel> _enrollmentDAL;

        public EnrollmentBL()
        {
            _enrollmentDAL = new EnrollmentDAL();
        }
        public EnrollmentBL(IDAL<EnrollmentModel> enrollmentDAL)
        {
            _enrollmentDAL = enrollmentDAL;
        }

        public IEnumerable<EnrollmentModel> GetAllEnrollments() { throw new NotImplementedException(); }
        public EnrollmentModel GetEnrollmentById(int EnrollmentId) { throw new NotImplementedException(); }
        public bool AddEnrollment(EnrollmentModel model) { throw new NotImplementedException(); }
        public bool UpdateEnrollment(EnrollmentModel model) { throw new NotImplementedException(); }
        public bool DeleteEnrollment(int EnrollmentId) { throw new NotImplementedException(); }
    }
}
