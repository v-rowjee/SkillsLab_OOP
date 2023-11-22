using SkillsLab_OOP.BL;
using SkillsLab_OOP.DAL.Common;
using SkillsLab_OOP.Enums;
using SkillsLab_OOP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SkillsLab_OOP.DAL
{
    public interface IEnrollmentDAL
    {
        IEnumerable<EnrollmentModel> GetAllEnrollments();
        EnrollmentModel GetEnrollmentById(int enrollmentId);
        bool AddEnrollment(EnrollmentModel model);
        bool UpdateEnrollment(EnrollmentModel model);
        bool DeleteEnrollment(int enrollmentId);
    }
    public class EnrollmentDAL : IEnrollmentDAL
    {
        private const string GetEnrollmentQuery = @"
            SELECT EnrollmentId, EmployeeId, EnrollmentId, StatusId
            FROM [dbo].[Enrollment]
            WHERE [EnrollmentId] = @EnrollmentId
        ";
        private const string GetAllEnrollmentsQuery = @"
            SELECT EnrollmentId, EmployeeId, EnrollmentId, StatusId
            FROM [dbo].[Enrollment]
        ";
        private const string GetAllProofsQuery = @"
            SELECT ProofId, Attachment
            FROM Proof
            WHERE EnrollmentId = @EnrollmentId
        ";
        private const string GetAllReasonsQuery = @"
            SELECT Reason
            FROM DeclinedEnrollemnt
            WHERE EnrollmentId = @EnrollmentId
        ";
        private const string AddEnrollmentQuery = @"
            INSERT [dbo].[Enrollment] (EmployeeId, EnrollmentId, StatusId) VALUES (@EmployeeId, @EnrollmentId, @StatusId);
        ";
        private const string UpdateEnrollmentQuery = @"
            UPDATE [dbo].[Enrollment]
            SET EmployeeId=@EmployeeId, EnrollmentId=@EnrollmentId, StatusId=@StatusId
            WHERE EnrollmentId=@EnrollmentId;
        ";
        private const string DeleteEnrollmentQuery = @"
            DELETE FROM [dbo].[DeclinedEnrollment] WHERE EnrollmentId=@EnrollmentId;
            DELETE FROM [dbo].[Proof] WHERE EnrollmentId=@EnrollmentId;
            DELETE FROM [dbo].[Enrollment] WHERE EnrollmentId=@EnrollmentId
        ";
        public IEnumerable<EnrollmentModel> GetAllEnrollments()
        {
            var Enrollments = new List<EnrollmentModel>();
            EnrollmentModel enrollment;

            var dt = DBCommand.GetData(GetAllEnrollmentsQuery);
            foreach (DataRow row in dt.Rows)
            {
                enrollment = new EnrollmentModel();
                enrollment.EnrollmentId = int.Parse(row["EnrollmentId"].ToString());

                var employee = new EmployeeModel();
                employee.EmployeeId = employee.EmployeeId = int.Parse(row["EmployeeId"].ToString());
                employee.FirstName = row["FirstName"].ToString();
                employee.LastName = row["LastName"].ToString();
                employee.NIC = row["NIC"].ToString();
                employee.PhoneNumber = row["PhoneNumber"].ToString();
                var department = new DepartmentModel();
                department.DepartmentId = int.Parse(row["DepartmentId"].ToString());
                department.Title = row["Title"].ToString();
                employee.Department = department;
                employee.Role = (RoleEnum)int.Parse(row["RoleId"].ToString());
                enrollment.Employee = employee;

                var training = new TrainingModel();
                training.TrainingId = int.Parse(row["TrainingId"].ToString());
                training.Title = row["Title"].ToString();
                training.Deadline = DateTime.Parse(row["Datetime"].ToString());
                training.Capacity = int.Parse(row["Capacity"].ToString());

                var trainingdepartment = new DepartmentModel();
                trainingdepartment.DepartmentId = int.Parse(row["TrainingDepartmentId"].ToString());
                trainingdepartment.Title = row["Title"].ToString();
                training.PriorityDepartment = trainingdepartment;
                enrollment.Training = training;

                var parameters = new List<SqlParameter> { new SqlParameter("@EnrollemntId", enrollment.EnrollmentId) };

                var proofs = new List<ProofModel>();
                ProofModel proof;
                var dt2 = DBCommand.GetDataWithCondition(GetAllProofsQuery, parameters);
                foreach (DataRow row2 in dt2.Rows)
                {
                    proof = new ProofModel();
                    proof.ProofId = int.Parse(row2["ProofId"].ToString());
                    proof.Attachment = row2["Attachment"].ToString();
                }

                var reason = new DeclinedEnrollmentModel();
                var dt3 = DBCommand.GetDataWithCondition(GetAllReasonsQuery, parameters);
                foreach (DataRow row3 in dt3.Rows)
                {
                    reason.DeclinedEnrollmentId = int.Parse(row3["DeclinedEnrollmentId"].ToString());
                    reason.Reason = row3["Reason"].ToString();
                }

                enrollment.Status =  (StatusEnum) int.Parse(row["StatusId"].ToString());

                Enrollments.Add(enrollment);
            }
            return Enrollments;
        }

        public EnrollmentModel GetEnrollmentById(int EnrollmentId)
        {
            var enrollment = new EnrollmentModel();
            var parameters = new List<SqlParameter> { new SqlParameter("@EnrollemntId", enrollment.EnrollmentId) };

            var dt = DBCommand.GetDataWithCondition(GetEnrollmentQuery, parameters);
            foreach (DataRow row in dt.Rows)
            {
                enrollment.EnrollmentId = int.Parse(row["EnrollmentId"].ToString());

                var employee = new EmployeeModel();
                employee.EmployeeId = employee.EmployeeId = int.Parse(row["EmployeeId"].ToString());
                employee.FirstName = row["FirstName"].ToString();
                employee.LastName = row["LastName"].ToString();
                employee.NIC = row["NIC"].ToString();
                employee.PhoneNumber = row["PhoneNumber"].ToString();
                var department = new DepartmentModel();
                department.DepartmentId = int.Parse(row["DepartmentId"].ToString());
                department.Title = row["Title"].ToString();
                employee.Department = department;
                employee.Role = (RoleEnum)int.Parse(row["RoleId"].ToString());
                enrollment.Employee = employee;

                var training = new TrainingModel();
                training.TrainingId = int.Parse(row["TrainingId"].ToString());
                training.Title = row["Title"].ToString();
                training.Deadline = DateTime.Parse(row["Datetime"].ToString());
                training.Capacity = int.Parse(row["Capacity"].ToString());

                var trainingdepartment = new DepartmentModel();
                trainingdepartment.DepartmentId = int.Parse(row["TrainingDepartmentId"].ToString());
                trainingdepartment.Title = row["Title"].ToString();
                training.PriorityDepartment = trainingdepartment;
                enrollment.Training = training;

                var proofs = new List<ProofModel>();
                ProofModel proof;
                var dt2 = DBCommand.GetDataWithCondition(GetAllProofsQuery, parameters);
                foreach (DataRow row2 in dt2.Rows)
                {
                    proof = new ProofModel();
                    proof.ProofId = int.Parse(row2["ProofId"].ToString());
                    proof.Attachment = row2["Attachment"].ToString();
                }

                var reason = new DeclinedEnrollmentModel();
                var dt3 = DBCommand.GetDataWithCondition(GetAllReasonsQuery, parameters);
                foreach (DataRow row3 in dt3.Rows)
                {
                    reason.DeclinedEnrollmentId = int.Parse(row3["DeclinedEnrollmentId"].ToString());
                    reason.Reason = row3["Reason"].ToString();
                }

                enrollment.Status = (StatusEnum)int.Parse(row["StatusId"].ToString());
            }
            return enrollment;
        }

        public bool AddEnrollment(EnrollmentModel model)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Employee", model.Employee));
            parameters.Add(new SqlParameter("@Training", model.Training));
            parameters.Add(new SqlParameter("@Proofs", model.Proofs));
            parameters.Add(new SqlParameter("@Status", model.Status));

            var EnrollmentInserted = DBCommand.InsertUpdateData(AddEnrollmentQuery, parameters);

            return EnrollmentInserted;
        }

        public bool UpdateEnrollment(EnrollmentModel model)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@EnrollmentId", model.EnrollmentId));
            parameters.Add(new SqlParameter("@Title", model.EmployeeId));
            parameters.Add(new SqlParameter("@Deadline", model.TrainingId));
            parameters.Add(new SqlParameter("@Capacity", model.Status));

            var EnrollmentUpdated = DBCommand.InsertUpdateData(UpdateEnrollmentQuery, parameters);

            return EnrollmentUpdated;
        }

        public bool DeleteEnrollment(int EnrollmentId)
        {
            var parameter = new SqlParameter("@EnrollmentId", EnrollmentId);
            return DBCommand.DeleteData(DeleteEnrollmentQuery, parameter);
        }

    }
}
