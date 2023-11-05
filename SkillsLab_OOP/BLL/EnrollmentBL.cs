using SkillsLab_OOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillsLab_OOP.BLL
{
    public interface IEnrollmentBL
    {
        void GetEnrollmentById(int EnrollmentId);
        void GetAllEnrollments();
        void AddEnrollment(EnrollmentModel model);
        void UpdateEnrollment(EnrollmentModel model);
        void DeleteEnrollment(int EnrollmentId);
    }
    public class EnrollmentBL : IEnrollmentBL
    {
        public void GetEnrollmentById(int EnrollmentId) { }
        public void GetAllEnrollments() { }
        public void AddEnrollment(EnrollmentModel model) { }
        public void UpdateEnrollment(EnrollmentModel model) { }
        public void DeleteEnrollment(int EnrollmentId) { }
    }
}
