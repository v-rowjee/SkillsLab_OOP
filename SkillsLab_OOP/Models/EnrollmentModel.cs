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
        public AccountModel Employee { get; set; }
        public TrainingModel Training { get; set; }
        public List<EnrollmentProofModel> EnrollmentProofs { get; set; }
        public string Status { get; set; }
        public string? Comment { get; set; }
    }

    public enum EnumStatus
    {
        Pending,
        Approved,
        Denied
    }
}
