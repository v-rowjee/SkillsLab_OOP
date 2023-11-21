﻿using SkillsLab_OOP.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillsLab_OOP.Models
{
    public class EnrollmentModel
    {
        public int EnrollmentId { get; set; }
        public int EmployeeId { get; set; }
        public int TrainingId { get; set; }
        public StatusEnum Status { get; set; }
    }
}
