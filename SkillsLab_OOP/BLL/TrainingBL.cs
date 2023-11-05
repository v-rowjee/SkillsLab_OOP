using SkillsLab_OOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillsLab_OOP.BLL
{
    public interface ITrainingBL
    {
        void GetAllTrainings();
        void GetTrainingById(int TrainingId);
        void AddTraining(TrainingModel model);
        void EditTraining(TrainingModel model);
        void DeleteTraining(int TrainingId);
    }
    public class TrainingBL : ITrainingBL
    {
        public void GetAllTrainings() { }
        public void GetTrainingById(int TrainingId) { }
        public void AddTraining(TrainingModel model) { }
        public void EditTraining(TrainingModel model) { }
        public void DeleteTraining(int TrainingId) {  }
    }
}
