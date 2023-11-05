using SkillsLab_OOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillsLab_OOP.BLL
{
    public interface IAccountBL
    {
        void GetAccountById(int AccountId);
        void GetAllAccounts();
    }
    public class AccountBL : IAccountBL
    {
        public void GetAccountById(int AccountId) { }
        public void GetAllAccounts() { }
    }
}
