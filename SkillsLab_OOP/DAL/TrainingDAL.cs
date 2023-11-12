using SkillsLab_OOP.Models;
using SkillsLab_OOP.DAL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillsLab_OOP.DAL
{
    public interface ITrainingDAL
    {
        IEnumerable<TrainingModel> GetAll();
        TrainingModel GetById(int TrainingId);
        bool Add(TrainingModel model);
        TrainingModel Update(TrainingModel model);
        bool Delete(int TrainingId);
    }
    public class TrainingDAL : ITrainingDAL
    {
        public IEnumerable<TrainingModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public TrainingModel GetById(int TrainingId)
        {
            throw new NotImplementedException();
        }

        public bool Add(TrainingModel model)
        {
            throw new NotImplementedException();
        }

        public TrainingModel Update(TrainingModel model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int TrainingId)
        {
            throw new NotImplementedException();
        }
    }
}
