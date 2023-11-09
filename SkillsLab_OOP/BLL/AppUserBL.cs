using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillsLab_OOP.DAL.Common
{
    public interface IAppUserBL
    {
        bool LoginUser();
        bool RegisterUser();
    }
    internal class AppUserBL : IAppUserBL
    {
        public bool LoginUser() { throw new NotImplementedException(); }
        public bool RegisterUser() { throw new NotImplementedException(); }
    }
}
