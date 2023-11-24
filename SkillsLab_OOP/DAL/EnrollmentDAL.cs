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
        bool AddDeclinedEnrollment(EnrollmentModel model);
        bool UpdateEnrollment(EnrollmentModel model);
        bool DeleteEnrollment(int enrollmentId);
    }
    public class EnrollmentDAL : IEnrollmentDAL
    {
        private const string GetEnrollmentQuery = @"
            SELECT e.EnrollmentId, e.EmployeeId, e.TrainingId, e.StatusId, emp.FirstName, emp.LastName, emp.NIC, emp.PhoneNumber, emp.DepartmentId, ed.Title, emp.RoleId, t.Title as TrainingTitle, t.Deadline, t.Capacity, t.PriorityDepartmentId, td.Title as TrainingDepartmentTitle
            FROM [dbo].[Enrollment] e
            INNER JOIN Employee emp ON e.EmployeeId=emp.EmployeeId
            INNER JOIN Department ed ON emp.DepartmentId=ed.DepartmentId
            INNER JOIN Training t ON e.TrainingId=t.TrainingId
            INNER JOIN Department td ON t.PriorityDepartmentId=td.DepartmentId
            WHERE EnrollmentId=@EnrollmentId
        ";
        private const string GetAllEnrollmentsQuery = @"
            SELECT e.EnrollmentId, e.EmployeeId, e.TrainingId, e.StatusId, emp.FirstName, emp.LastName, emp.NIC, emp.PhoneNumber, emp.DepartmentId, ed.Title, emp.RoleId, t.Title as TrainingTitle, t.Deadline, t.Capacity, t.PriorityDepartmentId, td.Title as TrainingDepartmentTitle
            FROM [dbo].[Enrollment] as e
            INNER JOIN Employee emp ON e.EmployeeId=emp.EmployeeId
            INNER JOIN Department ed ON emp.DepartmentId=ed.DepartmentId
            INNER JOIN Training t ON e.TrainingId=t.TrainingId
            INNER JOIN Department td ON t.PriorityDepartmentId=td.DepartmentId
        ";
        private const string GetAllProofsQuery = @"
            SELECT ProofId, Attachment
            FROM Proof
            WHERE EnrollmentId = @EnrollmentId
        ";
        private const string GetAllReasonsQuery = @"
            SELECT Reason
            FROM DeclinedEnrollment
            WHERE EnrollmentId = @EnrollmentId
        ";
        private const string AddEnrollmentQuery = @"
            INSERT [dbo].[Enrollment] (EmployeeId, TrainingId, StatusId) VALUES (@EmployeeId, @TrainingId, @StatusId);
        ";
        private const string AddProofQuery = @"
            INSERT [dbo].[Proof] (EnrollmentId, Attachment) VALUES (@EnrollmentId, @Attachment);
        ";
        private const string AddDeclinedEnrolment = @"
            INSERT [dbo].[DeclinedEnrolment] (EnrollmentId, Reason) VALUES (@EnrollmentId, @Reason);
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
                training.Title = row["TrainingTitle"].ToString();
                training.Deadline = DateTime.Parse(row["Datetime"].ToString());
                training.Capacity = int.Parse(row["Capacity"].ToString());

                var trainingdepartment = new DepartmentModel();
                trainingdepartment.DepartmentId = int.Parse(row["TrainingDepartmentId"].ToString());
                trainingdepartment.Title = row["TrainingDepartmentTitle"].ToString();
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

        public EnrollmentModel GetEnrollmentById(int enrollmentId)
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
                training.Title = row["TrainingTitle"].ToString();
                training.Deadline = DateTime.Parse(row["Datetime"].ToString());
                training.Capacity = int.Parse(row["Capacity"].ToString());

                var trainingdepartment = new DepartmentModel();
                trainingdepartment.DepartmentId = int.Parse(row["PriorityDepartmentId"].ToString());
                trainingdepartment.Title = row["TrainingDepartmentTitle"].ToString();
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
            parameters.Add(new SqlParameter("@EmployeeId", model.Employee.EmployeeId));
            parameters.Add(new SqlParameter("@FirstName", model.Employee.FirstName));
            parameters.Add(new SqlParameter("@LastName", model.Employee.LastName));
            parameters.Add(new SqlParameter("@NIC", model.Employee.NIC));
            parameters.Add(new SqlParameter("@PhoneNumber", model.Employee.PhoneNumber));
            parameters.Add(new SqlParameter("@DepartmentId", model.Employee.Department.DepartmentId));
            parameters.Add(new SqlParameter("@RoleId", (int)model.Employee.Role));

            parameters.Add(new SqlParameter("@TrainingId", model.Training.TrainingId));
            parameters.Add(new SqlParameter("@Deadline", model.Training.Deadline));
            parameters.Add(new SqlParameter("@Capacity", model.Training.Capacity));
            parameters.Add(new SqlParameter("@PriorityDepartmentId", model.Training.PriorityDepartment.DepartmentId));

            parameters.Add(new SqlParameter("@StatusId", (int) model.Status));

            if(model.Proofs != null)
            {
                foreach (var proof in model.Proofs)
                {
                    var parameters2 = new List<SqlParameter>();
                    parameters2.Add(new SqlParameter("@EnrollmentId", model.EnrollmentId));
                    parameters2.Add(new SqlParameter("@Attachment", proof.Attachment));

                    DBCommand.InsertUpdateData(AddProofQuery, parameters2);
                }
            }
            

            var EnrollmentInserted = DBCommand.InsertUpdateData(AddEnrollmentQuery, parameters);

            return EnrollmentInserted;
        }

        public bool AddDeclinedEnrollment(EnrollmentModel model)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@EnrollmentId", model.EnrollmentId));
            parameters.Add(new SqlParameter("@Reason", model.ReasonForDecline));

            var DeclinedEnrollmentInserted = DBCommand.InsertUpdateData(AddDeclinedEnrolment, parameters);

            return DeclinedEnrollmentInserted;
        }

        public bool UpdateEnrollment(EnrollmentModel model)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@EnrollmentId", model.EnrollmentId));
            parameters.Add(new SqlParameter("@Status", (int) model.Status));


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
