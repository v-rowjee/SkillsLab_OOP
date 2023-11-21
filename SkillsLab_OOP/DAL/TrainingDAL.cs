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

namespace SkillsLab_OOP.DAL
{
    public class TrainingDAL : IDAL<TrainingModel>
    {
        private const string GetTrainingQuery = @"
            SELECT TrainingId, Title, Deadline, Capacity
            FROM [dbo].[Training]
            WHERE [TrainingId] = @TrainingId
        ";
        private const string GetAllTrainingsQuery = @"
            SELECT TrainingId, Title, Deadline, Capacity, PriorityDepartmentId
            FROM [dbo].[Training]
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
        public IEnumerable<TrainingModel> GetAll()
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
                training.PriorityDepartmentId = int.Parse(row["PriorityDepartmentId"].ToString());

                trainings.Add(training);
            }
            return trainings;
        }

        public TrainingModel GetById(int trainingId)
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
                training.PriorityDepartmentId = int.Parse(row["PriorityDepartmentId"].ToString());
            }
            return training;
        }

        public bool Add(TrainingModel model)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Title", model.Title));
            parameters.Add(new SqlParameter("@Deadline", model.Deadline));
            parameters.Add(new SqlParameter("@Capacity", model.Capacity));
            parameters.Add(new SqlParameter("@PriorityDepartmentId", model.PriorityDepartmentId));

            var trainingInserted = DBCommand.InsertUpdateData(AddTrainingQuery, parameters);

            return trainingInserted;

        }

        public bool Update(TrainingModel model)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@TrainingId", model.TrainingId));
            parameters.Add(new SqlParameter("@Title", model.Title));
            parameters.Add(new SqlParameter("@Deadline",model.Deadline));
            parameters.Add(new SqlParameter("@Capacity", model.Capacity));
            parameters.Add(new SqlParameter("@PriorityDepartmentId", model.PriorityDepartmentId));
            var trainingUpdated = DBCommand.InsertUpdateData(UpdateTrainingQuery, parameters);

            return trainingUpdated;
        }

        public bool Delete(int trainingId)
        {
            var parameter = new SqlParameter("@TrainingId", trainingId);
            return DBCommand.DeleteData(DeleteTrainingQuery, parameter);
        }
    }
}
