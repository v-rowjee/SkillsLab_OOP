using SkillsLab_OOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillsLab_OOP.DAL.Common
{
    public interface IAccountBL
    {
        IEnumerable<AccountModel> GetAllAccounts();
        AccountModel GetAccountById(int AccountId);
        bool UpdateAccount(AccountModel account);
        bool DeleteAccount(int AccountId);
    }
    public class AccountBL : IAccountBL
    {
        public readonly IAccountDAL _accountDAL;

        public AccountBL(IAccountDAL AccountDAL)
        {
            _accountDAL = AccountDAL;
        }
        public AccountBL()
        {
            _accountDAL = new AccountDAL();
        }

        public AccountModel GetAccountById(int AccountId) { throw new NotImplementedException(); }
        public IEnumerable<AccountModel> GetAllAccounts() { throw new NotImplementedException(); }
        public bool UpdateAccount(AccountModel account) {  throw new NotImplementedException(); }
        public bool DeleteAccount(int AccountId) { throw new NotImplementedException(); }
    }
}
