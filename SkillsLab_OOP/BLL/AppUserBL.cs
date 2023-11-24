using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoHelper;
using SkillsLab_OOP.DAL;
using SkillsLab_OOP.Models.ViewModels;

namespace SkillsLab_OOP.BL
{
    public interface IAppUserBL
    {
        bool AuthenticateUser(LoginViewModel model);
        bool RegisterUser(RegisterViewModel model);
    }
    public class AppUserBL : IAppUserBL
    {
        public readonly IAppUserDAL _appUserDAL;
        public readonly IEmployeeDAL _employeeDAL;

        public AppUserBL(IAppUserDAL appUserBL, IEmployeeDAL accountDAL)
        {
            _appUserDAL = appUserBL;
            _employeeDAL = accountDAL;
        }
        public AppUserBL()
        {
            _appUserDAL = new AppUserDAL();
            _employeeDAL = new EmployeeDAL();
        }

        public bool AuthenticateUser(LoginViewModel model)
        {
            var hashedPassword = _appUserDAL.GetHashedPassword(model);
            if (hashedPassword != null)
            {
                return VerifyPassword(hashedPassword, model.Password);
            }
            return false;
        }

        public bool RegisterUser(RegisterViewModel model)
        {
            if (ValidateDuplicatedEmail(model.Email))
            {
                model.Password = HashPassword(model.Password);
                return _appUserDAL.RegisterUser(model);
            }
            //return "DuplicatedEmail";
            return false;
        }

        private bool ValidateDuplicatedEmail(string email)
        {
            var accountsWithSameEmail = _appUserDAL.GetAllAppUserEmail().Contains(email.Trim());

            return accountsWithSameEmail;
        }

        private string HashPassword(string password)
        {
            return Crypto.HashPassword(password);
        }

        private bool VerifyPassword(string hash, string password)
        {
            return Crypto.VerifyHashedPassword(hash, password);
        }
    }
}
