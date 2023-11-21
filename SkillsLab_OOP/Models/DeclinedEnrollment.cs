using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillsLab_OOP.Models
{
    public class DeclinedEnrollment
    {
        public int DeclinedEnrollmentId { get; set; }
        public int EnrollmentId { get; set;}
        public string Reason { get; set;}
    }
}
