﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillsLab_OOP.Models
{
    public class TrainingModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DepartmentModel? PriorityDepartment { get; set; }
        public int Capacity { get; set; }
        public List<PreRequisiteModel> PreRequisites { get; set; }
        public DateTime Deadline { get; set; }
    }
}
