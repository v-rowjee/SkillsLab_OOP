using SkillsLab_OOP.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillsLab_OOP.Models.ViewModels
{
    public class RegisterViewModel
    {
        public string Name { get; set; }
        public DepartmentModel Department { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string NIC { get; set; }
        public string PhoneNumber { get; set; }
        public RoleEnum Role { get; set; }
    }
}
