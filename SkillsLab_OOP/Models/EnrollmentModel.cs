using SkillsLab_OOP.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillsLab_OOP.Models
{
    public class EnrollmentModel
    {
        public int Id { get; set; }
        public EmployeeModel Employee { get; set; }
        public TrainingModel Training { get; set; }
        public List<ProofModel> Proofs { get; set; }
        public StatusEnum Status { get; set; }
        public string? Comment { get; set; }
    }
}
