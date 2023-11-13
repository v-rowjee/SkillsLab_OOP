using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillsLab_OOP.Models
{
    public class ProofModel
    {
        public int Id { get; set; }
        public PreRequisiteModel PreRequisite { get; set; }
        public string Attachment {  get; set; }
    }
}
