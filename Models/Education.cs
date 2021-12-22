using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MCC61_API_Project.Models
{
    [Table("tb_m_education")]
    public class Education
    {
        [Key]
        public int EducationID { get; set; }
        public string Degree { get; set; }
        public float GPA { get; set; }

        //relasi dengan University
        public University University { get; set; }
        public int UniversityID { get; set; }

        //relasi dengan profiling
        public ICollection<Profiling> Profilings { get; set; }
    }
}
