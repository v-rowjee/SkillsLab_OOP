using SkillsLab_OOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillsLab_OOP.DAL.Common
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
        public IEnumerable<EnrollmentModel> GetAllEnrollments() { throw new NotImplementedException(); }
        public EnrollmentModel GetEnrollmentById(int EnrollmentId) { throw new NotImplementedException(); }
        public bool AddEnrollment(EnrollmentModel model) { throw new NotImplementedException(); }
        public bool UpdateEnrollment(EnrollmentModel model) { throw new NotImplementedException(); }
        public bool DeleteEnrollment(int EnrollmentId) { throw new NotImplementedException(); }
    }
}
