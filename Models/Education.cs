using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MCC61_API_Project.Models
{
    [Table("tb_m_education")]
    public class Education
    {
        [Key]
        public int EducationID { get; set; }
        [Required(ErrorMessage ="Degree is Required")]
        public string Degree { get; set; }
        [Required(ErrorMessage = "GPA is Required")]
        public float GPA { get; set; }

        //relasi dengan University
        [JsonIgnore]
        public virtual University University { get; set; }
        [Required(ErrorMessage = "UniversityID is Required")]
        public int UniversityID { get; set; }

        //relasi dengan profiling
        [JsonIgnore]
        public virtual ICollection<Profiling> Profilings { get; set; }
    }
}
