﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillsLab_OOP.Models
{
    public class AppUserModel
    {
        public int AppUserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public EmployeeModel Employee { get; set; }
    }
}
