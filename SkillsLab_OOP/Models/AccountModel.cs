using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillsLab_OOP.Models
{
    public class AccountModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DepartmentModel Department { get; set; }
        public string Email { get; set; }
        public string NIC { get; set; }
        public string PhoneNumber { get; set; }
        public EnumRole Role { get; set; }
    }

    public enum EnumRole
    {
        Employee = 0,
        Manager = 1,
        Admin = 2
    }
}
