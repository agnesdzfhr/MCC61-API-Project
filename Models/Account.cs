using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MCC61_API_Project.Models
{
    [Table("tb_tr_account")]
    public class Account
    {
        [Key]
        public string NIK { get; set; }

        //[Required(ErrorMessage ="Password is Required")]
        //[RegularExpression("^(?=.*[a - z])(?=.*[A - Z])(?=.*[0 - 9])(?=.*[!@#$%^&*_-])(?=.{8,})", ErrorMessage = "Password must containing at least 8 character, 1 uppercase char(a-z), 1 lowercase char (A-Z), 1 number(0-9), and 1 special char(!@#$%^&*_-)")]
        public string password { get; set; }

        // untuk relasi ke employee
        public Employee Employee;
    }
}
