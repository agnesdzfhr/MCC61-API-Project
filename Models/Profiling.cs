using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MCC61_API_Project.Models
{
    [Table("tb_tr_profiling")]
    public class Profiling
    {
        [Key]
        public string NIK { get; set; }
        
        //relasi dengan Account
        public Account Account { get; set; }

        //relasi dengan Education
        public Education Education { get; set; }
        public int EducationId { get; set; }

    }
}
