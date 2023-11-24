using Newtonsoft.Json;
using SkillsLab_OOP;
using SkillsLab_OOP.DAL;
using SkillsLab_OOP.DAL.Common;
using SkillsLab_OOP.Models;
using SkillsLab_OOP.Models.ViewModels;
using System.Diagnostics;

public class Program
{
    public static void Main(string[] args)
    {
        AppUserDAL appUserDAL = new AppUserDAL();
        EmployeeDAL employeeDAL = new EmployeeDAL();
        EnrollmentDAL enrollmentDAL = new EnrollmentDAL();
        TrainingDAL trainingDAL = new TrainingDAL();

        // Register Employee
        //              RegisterEmployee(appUserDAL);
        // Login User as Employee
        // View All Training
        // Enroll in a Training
        // View Enrolled Training
        // Cancel Enrollment from Training

        // Login as Manager
        // View All his Trainings
        // View All Enrollments from a Training
        // Accept Enrollment
        // Decline Enrollment

        // Login as Admin
        // Create a Training with priority
        // Create a Training without priority



        //var dal = trainingDAL.GetAllTrainings().ToList();
        //dal.ForEach(x => { Console.WriteLine(JsonConvert.SerializeObject(x, Formatting.Indented)); });

        TrainingModel training = new TrainingModel();
        training.Capacity = 10;
        training.Deadline = DateTime.Now;
        training.Title = "Test";
        training.PriorityDepartment = new DepartmentModel { DepartmentId = 1 };
        var dal2 = trainingDAL.AddTraining(training);
        Console.WriteLine(JsonConvert.SerializeObject(dal2, Formatting.Indented));
    }

    public static void RegisterEmployee(AppUserDAL appUserDAL)
    {
        RegisterViewModel model = new RegisterViewModel();
        model.FirstName = "Ved";
        model.LastName = "Rowjee";
        model.Email = "ved.rowjee@gmail.com";


        appUserDAL.RegisterUser(model);
    }
}
