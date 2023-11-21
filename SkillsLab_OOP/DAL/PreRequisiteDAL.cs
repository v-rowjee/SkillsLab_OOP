using SkillsLab_OOP.BL;
using SkillsLab_OOP.DAL.Common;
using SkillsLab_OOP.Models;
using SkillsLab_OOP.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SkillsLab_OOP.DAL
{
    public interface IPreRequisite
    {
        IEnumerable<PreRequisiteModel> GetAllPreRequisitesByTrainingId(int trainingId);
        bool AddPreRequisites(int trainingId, PreRequisiteModel model);
        PreRequisiteModel UpdatePreRequisite(PreRequisiteModel model);
        bool DeletePreRequisite(int preRequisiteId);
        bool DeleteAllPreRequisitesByTrainingId(int trainingId);
    }
    public class PreRequisiteDAL : IPreRequisite
    {
        private const string AddPreRequisiteQuery = @"
            INSERT [dbo].[PreRequisite] (TrainingId, Detail) VALUES (@TrainingId, @Detail);
        ";

        public bool AddPreRequisites(int trainingId, PreRequisiteModel model)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@TrainingId", trainingId));
            parameters.Add(new SqlParameter("@Detail", model.Detail));

            var preRequisiteInserted = DBCommand.InsertUpdateData(AddPreRequisiteQuery, parameters);

            return preRequisiteInserted;
        }

        public bool DeleteAllPreRequisitesByTrainingId(int trainingId)
        {
            throw new NotImplementedException();
        }

        public bool DeletePreRequisite(int preRequisiteId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PreRequisiteModel> GetAllPreRequisitesByTrainingId(int trainingId)
        {
            throw new NotImplementedException();
        }

        public PreRequisiteModel UpdatePreRequisite(PreRequisiteModel model)
        {
            throw new NotImplementedException();
        }
    }
}
