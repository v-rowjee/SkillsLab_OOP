﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillsLab_OOP.Models
{
    public class PreRequisiteModel
    {
        public int PreRequisiteId { get; set; }
        public int TrainingId {  get; set; }
        public string Detail { get; set; }
    }
}
