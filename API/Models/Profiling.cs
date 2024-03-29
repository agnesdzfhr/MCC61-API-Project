﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MCC61_API_Project.Models
{
    [Table("tb_tr_profiling")]
    public class Profiling
    {
        [Key]
        public string NIK { get; set; }

        //relasi dengan Account
        [JsonIgnore]
        public virtual Account Account { get; set; }

        //relasi dengan Education
        [JsonIgnore]
        public virtual Education Education { get; set; }
        [Required(ErrorMessage ="EducationId is Required")]
        public int EducationId { get; set; }

    }
}
