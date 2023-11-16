using SkillsLab_OOP.DAL.Common;
using SkillsLab_OOP.Models;
using SkillsLab_OOP.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SkillsLab_OOP.DAL
{
    public interface IAppUserDAL
    {
        bool AuthenticateUser(LoginViewModel model);
        bool RegisterUser(RegisterViewModel model);
        string GetHashedPassword(LoginViewModel model);
    }
    public class AppUserDAL : IAppUserDAL
    {
        private const string AuthenticateUserQuery = @"
            SELECT emp.*
            FROM [dbo].[Employee] emp with(nolock) INNER JOIN [dbo].[AppUser] au with(nolock) ON emp.[EmployeeId]=au.[EmployeeId] 
            WHERE emp.[Email] = @Email AND au.[Password] = @Password ";
        private const string RegisterUserQuery = @"
            INSERT INTO [dbo].[Employee] ([FirstName] ,[LastName] ,[Email], [Password], [DepartmentId], [PhoneNumber] ,[NIC], [Role])
            VALUES (@FirstName ,@LastName, @DepartmentId ,@Email, @PhoneNumber ,@NIC, @Role);

            INSERT INTO [dbo].[AppUser] ([EmployeeId],[Password])
            VALUES ( SCOPE_IDENTITY() , @Password)";
        private const string GetHashedPasswordQuery = @"
            SELECT Password 
            FROM AppUser a INNER JOIN Employee e ON a.EmployeeId=e.EmployeeId
            WHERE e.Email=@Email";

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
            parameters.Add(new SqlParameter("@DepartmentId", model.DepartmentId));
            parameters.Add(new SqlParameter("@NIC", model.NIC));
            parameters.Add(new SqlParameter("@PhoneNumber", model.PhoneNumber));
            parameters.Add(new SqlParameter("@Role", model.Role));

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

    }
}