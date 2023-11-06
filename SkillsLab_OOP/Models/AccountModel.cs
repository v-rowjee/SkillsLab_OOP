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
        public int Phone { get; set; }
        public string Role { get; set; }
    }
}
