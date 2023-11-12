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
            SELECT acc.*
            FROM [dbo].[Account] acc with(nolock) INNER JOIN [dbo].[AppUser] au with(nolock) ON acc.[AccountId]=au.[AccountId] 
            WHERE acc.[Email] = @Email AND au.[Password] = @Password ";
        private const string RegisterUserQuery = @"
            DECLARE @LocId INT;
            IF NOT EXISTS (SELECT * FROM [dbo].[Location] WHERE [Region]=@Region AND [City]=@City AND [Country]=@Country)
                BEGIN
                    INSERT INTO [dbo].[Location] ([Region] ,[City] ,[Country])
                    VALUES (@Region ,@City ,@Country);
                    SET @LocId = SCOPE_IDENTITY()
                END
            ELSE 
                BEGIN
                    SET @LocId = (SELECT [LocationId] FROM [dbo].[Location] WHERE [Region]=@Region AND [City]=@City AND [Country]=@Country)
                END

            INSERT INTO [dbo].[Account] ([FirstName] ,[LastName] ,[Email], [Phone] ,[AddressId])
            VALUES (@FirstName ,@LastName ,@Email, @Phone ,@LocId);

            INSERT INTO [dbo].[AppUser] ([AccountId],[Password])
            VALUES ( SCOPE_IDENTITY() , @Password)";
        private const string GetHashedPasswordQuery = @"
            SELECT Password 
            FROM AppUser au INNER JOIN Account a ON au.AccountId=a.AccountId
            WHERE a.Email=@Email";

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
            parameters.Add(new SqlParameter("@Name", model.Name));
            parameters.Add(new SqlParameter("@NIC", model.NIC));
            parameters.Add(new SqlParameter("@PhoneNumber", model.PhoneNumber));
            
            parameters.Add(new SqlParameter("@Region", model.Department.Id));

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