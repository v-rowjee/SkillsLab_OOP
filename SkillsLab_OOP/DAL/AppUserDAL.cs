using SkillsLab_OOP.DAL.Common;
using SkillsLab_OOP.Models;
using SkillsLab_OOP.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace SkillsLab_OOP.DAL
{
    public interface IAppUserDAL
    {
        bool AuthenticateUser(LoginViewModel model);
        bool RegisterUser(RegisterViewModel model);
        string GetHashedPassword(LoginViewModel model);
        bool GetAllAppUserEmail();
    }
    public class AppUserDAL : IAppUserDAL
    {
        private const string AuthenticateUserQuery = @"
            SELECT e.EmployeeId
            FROM [dbo].[Employee] e 
            INNER JOIN [dbo].[AppUser] a ON e.[EmployeeId]=a.[EmployeeId] 
            WHERE a.[Email] = @Email AND a.[Password] = @Password 
        ";
        private const string RegisterUserQuery = @"
            INSERT [dbo].[Employee] ([FirstName] ,[LastName] ,[NIC] ,[PhoneNumber], [DepartmentId], [RoleId])
            VALUES (@FirstName ,@LastName, @NIC, @PhoneNumber, @DepartmentId, @RoleId);

            INSERT [dbo].[AppUser] (Email, Password, EmployeeId)
            VALUES (@Email, @Password, @@IDENTITY)
            ";
        private const string GetHashedPasswordQuery = @"
            SELECT Password 
            FROM [dbo].[AppUser] a 
            INNER JOIN Employee e ON a.EmployeeId=e.EmployeeId
            WHERE a.Email=@Email";

        private const string GetAllAppUserEmails = @"
            SELECT Email
            FROM AppUser
        ";

        public bool AuthenticateUser(LoginViewModel model)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Email", model.Email));
            parameters.Add(new SqlParameter("@Password", model.Password));

            var dt = DBCommand.GetDataWithCondition(AuthenticateUserQuery, parameters);

            return dt.Rows.Count > 0;
        }

        public bool RegisterUser(RegisterViewModel model)
        {
            var result = false;

            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Email", model.Email));
            parameters.Add(new SqlParameter("@Password", model.Password));
            parameters.Add(new SqlParameter("@FirstName", model.FirstName));
            parameters.Add(new SqlParameter("@LastName", model.LastName));
            parameters.Add(new SqlParameter("@NIC", model.NIC));
            parameters.Add(new SqlParameter("@PhoneNumber", model.PhoneNumber));
            parameters.Add(new SqlParameter("@DepartmentId", model.DepartmentId));
            parameters.Add(new SqlParameter("@RoleId", (int) model.Role));

            result = DBCommand.InsertUpdateData(RegisterUserQuery, parameters);


            return result;
        }

        public string GetHashedPassword(LoginViewModel model)
        {
            string? hashedPassword = "";
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Email", model.Email));

            var dt = DBCommand.GetDataWithCondition(GetHashedPasswordQuery, parameters);

            foreach (DataRow row in dt.Rows)
            {
                hashedPassword = row["Password"].ToString();
            }

            return hashedPassword;
        }

        public bool GetAllAppUserEmail()
        {
            var dt = DBCommand.GetData(GetAllAppUserEmails);
            return dt.Rows.Count > 0;
        }
    }
}