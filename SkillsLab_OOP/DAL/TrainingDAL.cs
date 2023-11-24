using SkillsLab_OOP.Models;
using SkillsLab_OOP.DAL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SkillsLab_OOP.BL;
using System.Data.SqlClient;
using System.Reflection;
using SkillsLab_OOP.Models.ViewModels;

namespace SkillsLab_OOP.DAL
{
    public interface ITrainingDAL
    {
        IEnumerable<TrainingModel> GetAllTrainings();
        TrainingModel GetTrainingById(int trainingId);
        bool AddTraining(TrainingModel model);
        bool UpdateTraining(TrainingModel model);
        bool DeleteTraining(int trainingId);
        bool AddPreRequisite(PreRequisiteModel model);
        bool DeletePreRequisite(int preRequisiteId);
    }
    public class TrainingDAL : ITrainingDAL
    {
        private const string GetTrainingQuery = @"
            SELECT TrainingId, Title, Deadline, Capacity, t.PriorityDepartmentId, d.Title
            FROM [dbo].[Training] as t 
            INNER JOIN department as d
            ON t.PriorityDepartmentId = d.DepartmentId
            WHERE [TrainingId] = @TrainingId
        ";
        private const string GetAllTrainingsQuery = @"
            SELECT TrainingId, Title, Deadline, Capacity, t.PriorityDepartmentId, d.Title
            FROM [dbo].[Training] as t 
            INNER JOIN department as d
            ON t.PriorityDepartmentId = d.DepartmentId
        ";
        private const string GetAllPreRequisitesQuery = @"
            SELECT tp.PreRequisiteId, p.Detail
            FROM TrainingPreRequisite as tp
            INNER JOIN PreRequisite as p
            ON tp.PreRequisiteId = p.PreRequisiteId
            WHERE tp.TrainingId = @TrainingId
        ";
        private const string AddTrainingQuery = @"
            INSERT [dbo].[Training] (Title, Deadline, Capacity, PriorityDepartmentId) VALUES (@Title, @Deadline, @Capacity, @PriorityDepartmentId);
        ";
        private const string UpdateTrainingQuery = @"
            UPDATE [dbo].[Training]
            SET Title=@Title, Deadline=@Deadline, Capacity=@Capacity, PriorityDepartment=@PriorityDepartment
            WHERE TrainingId=@TrainingId;
        ";
        private const string DeleteTrainingQuery = @"
            DELETE FROM [dbo].[TrainingPreRequisite] WHERE TrainingId=@TrainingId;
            DELETE FROM [dbo].[Enrollment] WHERE TrainingId=@TrainingId;
            DELETE FROM [dbo].[Training] WHERE TrainingId=@TrainingId
        ";

        private const string AddPreRequisiteQuery = @"
            DECLARE @PreRequisiteId INT
            IF EXISTS (SELECT @PreRequisiteId=PreRequisiteId from PreRequisite WHERE Detail = @Detail)
            BEGIN
                INSERT TrainingPreRequisite (TrainingId, PreRequisiteId) VALUES (@TrainingId,@PreRequisiteId)
            ELSE
                INSERT [dbo].[PreRequisite] (Detail) VALUES (@Detail)
                INSERT TrainingPreRequisite (TrainingId, PreRequisiteId) VALUES (@TrainingId,@@IDENTITY)
            END
        ";

        private const string DeletePreRequisiteQuery = @"
            DELETE FROM [dbo].[TrainingPreRequisite] WHERE PreRequisiteId=@PreRequisiteId
            DELETE FROM [dbo].[PreRequisite] WHERE PreRequisiteId=@PreRequisiteId
        ";
        public IEnumerable<TrainingModel> GetAllTrainings()
        {
            var trainings = new List<TrainingModel>();
            TrainingModel training;

            var dt = DBCommand.GetData(GetAllTrainingsQuery);
            foreach (DataRow row in dt.Rows)
            {
                training = new TrainingModel();
                training.TrainingId = int.Parse(row["TrainingId"].ToString());
                training.Title = row["Title"].ToString();
                training.Deadline = DateTime.Parse(row["Datetime"].ToString());
                training.Capacity = int.Parse(row["Capacity"].ToString());

                var department = new DepartmentModel();
                department.DepartmentId = int.Parse(row["DepartmentId"].ToString());
                department.Title = row["Title"].ToString();
                training.PriorityDepartment = department;

                var preRequisites = new List<PreRequisiteModel>();
                PreRequisiteModel preRequisite = new PreRequisiteModel();
                var dt2 = DBCommand.GetData(GetAllPreRequisitesQuery);
                foreach (DataRow row2 in dt2.Rows)
                {
                    preRequisite = new PreRequisiteModel();
                    preRequisite.PreRequisiteId = int.Parse(row["PreRequisiteId"].ToString());
                    preRequisite.Detail = row["Detail"].ToString();

                    preRequisites.Add(preRequisite);
                }
                training.PreRequisites = preRequisites;

                trainings.Add(training);
            }
            return trainings;
        }

        public TrainingModel GetTrainingById(int trainingId)
        {
            var training = new TrainingModel();
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@TrainingId", trainingId));

            var dt = DBCommand.GetDataWithCondition(GetTrainingQuery, parameters);
            foreach(DataRow row in dt.Rows)
            {
                training.TrainingId = int.Parse(row["TrainingId"].ToString());
                training.Title = row["Title"].ToString();
                training.Deadline = DateTime.Parse(row["Datetime"].ToString());
                training.Capacity = int.Parse(row["Capacity"].ToString());

                var department = new DepartmentModel();
                department.DepartmentId = int.Parse(row["DepartmentId"].ToString());
                department.Title = row["Title"].ToString();
                training.PriorityDepartment = department;

                var preRequisites = new List<PreRequisiteModel>();
                PreRequisiteModel preRequisite = new PreRequisiteModel();
                var dt2 = DBCommand.GetData(GetAllPreRequisitesQuery);
                foreach (DataRow row2 in dt2.Rows)
                {
                    preRequisite = new PreRequisiteModel();
                    preRequisite.PreRequisiteId = int.Parse(row["PreRequisiteId"].ToString());
                    preRequisite.Detail = row["Detail"].ToString();

                    preRequisites.Add(preRequisite);
                }
                training.PreRequisites = preRequisites;
            }
            return training;
        }

        public bool AddTraining(TrainingModel model)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Title", model.Title));
            parameters.Add(new SqlParameter("@Deadline", model.Deadline));
            parameters.Add(new SqlParameter("@Capacity", model.Capacity));
            parameters.Add(new SqlParameter("@PriorityDepartmentId", model.PriorityDepartment.DepartmentId));

            var trainingInserted = DBCommand.InsertUpdateData(AddTrainingQuery, parameters);

            return trainingInserted;

        }

        public bool UpdateTraining(TrainingModel model)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@TrainingId", model.TrainingId));
            parameters.Add(new SqlParameter("@Title", model.Title));
            parameters.Add(new SqlParameter("@Deadline",model.Deadline));
            parameters.Add(new SqlParameter("@Capacity", model.Capacity));
            parameters.Add(new SqlParameter("@PriorityDepartmentId", model.PriorityDepartment.DepartmentId));
            var trainingUpdated = DBCommand.InsertUpdateData(UpdateTrainingQuery, parameters);

            return trainingUpdated;
        }

        public bool DeleteTraining(int trainingId)
        {
            var parameter = new SqlParameter("@TrainingId", trainingId);
            return DBCommand.DeleteData(DeleteTrainingQuery, parameter);
        }

        public bool AddPreRequisite(PreRequisiteModel model)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@TrainingId", model.TrainingId));
            parameters.Add(new SqlParameter("@Detail", model.Detail.Trim()));

            var PreRequisiteInserted = DBCommand.InsertUpdateData(AddPreRequisiteQuery, parameters);
            return PreRequisiteInserted;
        }

        public bool DeletePreRequisite(int preRequisiteId)
        {
            var parameter = new SqlParameter("PreRequisiteId", preRequisiteId);
            return DBCommand.DeleteData(DeletePreRequisiteQuery, parameter);
        }
    }
}
