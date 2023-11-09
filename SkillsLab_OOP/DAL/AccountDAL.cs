using SkillsLab_OOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillsLab_OOP.DAL
{
    public interface IAccountDAL
    {
        IEnumerable<AccountModel> GetAll();
        AccountModel GetById(int accountId);
        bool Update(AccountModel model);
        bool Delete(int accountId);
    }
    internal class AccountDAL : IAccountDAL
    {
        public IEnumerable<AccountModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public AccountModel GetById(int accountId)
        {
            throw new NotImplementedException();
        }

        public bool Update(AccountModel model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int accountId)
        {
            throw new NotImplementedException();
        }
    }
}
