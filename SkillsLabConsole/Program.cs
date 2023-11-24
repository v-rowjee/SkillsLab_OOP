using SkillsLab_OOP;
using SkillsLab_OOP.DAL;
using SkillsLab_OOP.DAL.Common;

public class Program
{
    public static void Main(string[] args)
    {
        TrainingDAL trainingDAL = new TrainingDAL();
        var trainings = trainingDAL.GetAllTrainings().ToList();
        trainings.ForEach(x => { Console.WriteLine(x); });
    }
}
