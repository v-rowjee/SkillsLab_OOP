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
    public interface ITrainingBL
    {
        IEnumerable<TrainingModel> GetAllTrainings();
        TrainingModel GetTrainingById(int TrainingId);
        bool AddTraining(TrainingModel model);
        bool UpdateTraining(TrainingModel model);
        bool DeleteTraining(int TrainingId);
    }
    public class TrainingBL : ITrainingBL
    {
        private readonly IDAL<TrainingModel> _trainingDAL;

        public TrainingBL()
        {
            _trainingDAL = new TrainingDAL();
        }
        public TrainingBL(IDAL<TrainingModel> trainingDAL)
        {
            _trainingDAL = trainingDAL;
        }

        public IEnumerable<TrainingModel> GetAllTrainings() { throw new NotImplementedException(); }
        public TrainingModel GetTrainingById(int TrainingId) { throw new NotImplementedException(); }
        public bool AddTraining(TrainingModel model) { throw new NotImplementedException(); }
        public bool UpdateTraining(TrainingModel model) { throw new NotImplementedException(); }
        public bool DeleteTraining(int TrainingId) { throw new NotImplementedException(); }
    }
}
