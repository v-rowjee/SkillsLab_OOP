using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillsLab_OOP.BLL
{
    public interface IAppUserBL
    {
        void LoginUser();
        void RegisterUser();
    }
    internal class AppUserBL : IAppUserBL
    {
        public void LoginUser() { }
        public void RegisterUser() { }
    }
}
