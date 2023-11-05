using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillsLab_OOP.Models
{
    public class EnrollmentProofModel
    {
        public int Id { get; set; }
        public int EnrollmentId { get; set; }
        public string PreRequisite { get; set; }
        public string Attachment {  get; set; }
    }
}
