using SkillsLab_OOP.DAL.Common;
using SkillsLab_OOP.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillsLab_OOP.DAL
{
    public class TrainingPreRequisitePreRequisiteDAL : IDAL<PreRequisiteModel>
    {
        private const string GetTrainingPreRequisiteQuery = @"
            SELECT TrainingId, PreRequisiteId
            FROM [dbo].[TrainingPreRequisite]
            WHERE [TrainingId] = @TrainingId
        ";
        private const string GetAllTrainingPreRequisitesQuery = @"
            SELECT TrainingId, PreRequisiteId
            FROM [dbo].[TrainingPreRequisite]
        ";
        private const string AddTrainingPreRequisiteQuery = @"
            INSERT [dbo].[TrainingPreRequisite] (TrainingId, PreRequisiteId) VALUES (@TrainingId, @PreRequisiteId);
        ";
        private const string UpdateTrainingPreRequisiteQuery = @"
            UPDATE [dbo].[TrainingPreRequisite]
            SET TrainingId=@TrainingId, PreRequisiteId=@PreRequisiteId
            WHERE TrainingId=@TrainingId;
        ";
        private const string DeleteTrainingPreRequisiteQuery = @"
            DELETE FROM [dbo].[TrainingPreRequisite] WHERE TrainingId=@TrainingId
        ";
        public IEnumerable<TrainingPreRequisiteModel> GetAll()
        {
            var TrainingPreRequisites = new List<TrainingPreRequisiteModel>();
            TrainingPreRequisiteModel TrainingPreRequisite;

            var dt = DBCommand.GetData(GetAllTrainingPreRequisitesQuery);
            foreach (DataRow row in dt.Rows)
            {
                TrainingPreRequisite = new TrainingPreRequisiteModel();
                TrainingPreRequisite.TrainingId = int.Parse(row["TrainingId"].ToString());
                TrainingPreRequisite.PreRequisiteId = int.Parse(row["PreRequisiteId"].ToString());

                TrainingPreRequisites.Add(TrainingPreRequisite);
            }
            return TrainingPreRequisites;
        }

        public TrainingPreRequisiteModel GetById(int trainingId)
        {
            var TrainingPreRequisite = new TrainingPreRequisiteModel();
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@TrainingId", trainingId));

            var dt = DBCommand.GetDataWithCondition(GetTrainingPreRequisiteQuery, parameters);
            foreach (DataRow row in dt.Rows)
            {
                TrainingPreRequisite.TrainingId = int.Parse(row["TrainingId"].ToString());
                TrainingPreRequisite.PreRequisiteId = int.Parse(row["PreRequisiteId"].ToString());
            }
            return TrainingPreRequisite;
        }

        public bool Add(TrainingPreRequisiteModel model)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@TrainingId", model.TrainingId));
            parameters.Add(new SqlParameter("@PreRequisiteId", model.PreRequisiteId));

            var TrainingPreRequisiteInserted = DBCommand.InsertUpdateData(AddTrainingPreRequisiteQuery, parameters);

            return TrainingPreRequisiteInserted;

        }

        public bool Update(TrainingPreRequisiteModel model)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@TrainingId", model.TrainingId));
            parameters.Add(new SqlParameter("@PreRequisiteId", model.PreRequisiteId));
            var TrainingPreRequisiteUpdated = DBCommand.InsertUpdateData(UpdateTrainingPreRequisiteQuery, parameters);

            return TrainingPreRequisiteUpdated;
        }

        public bool Delete(int trainingId)
        {
            var parameter = new SqlParameter("@TrainingId", trainingId);
            return DBCommand.DeleteData(DeleteTrainingPreRequisiteQuery, parameter);
        }
    }
}
