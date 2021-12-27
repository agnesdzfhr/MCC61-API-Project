using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MCC61_API_Project.Models
{
    [Table("tb_tr_account")]
    public class Account
    {
        [Key]
        public string NIK { get; set; }

        [Required(ErrorMessage ="Password is Required")]
        //[RegularExpression("^(?=.*[a - z])(?=.*[A - Z])(?=.*[0 - 9])(?=.*[!@#$%^&*_-])(?=.{8,})", 
        //ErrorMessage = "Password must containing at least 8 character, 1 uppercase char(a-z), 1 lowercase char (A-Z), 1 number(0-9), and 1 special char(!@#$%^&*_-)")]
        public string Password { get; set; }
        public int? OTP { get; set; }
        [Display(Name = "Date")]
        public DateTime? ExpiredDate { get; set; }
        //public DateTime? ExpiredDate { get; set; } = DateTime.Now.AddMinutes(5);
        public bool? isUsed { get; set; }

        // untuk relasi ke employee
        [JsonIgnore]
        public virtual Employee Employee { get; set; }

        //relasi ke profiling
        [JsonIgnore]
        public virtual Profiling Profiling { get; set; }

        /*TODO
         * Bikin VM buat request password dan change password
         * Bikin request buat forgot password
         * di forgot password dilakukan generate otp, bikin expiredtoken sm isusednya
         * trus kirim otpnya ke email
         * trus bikin request buat change password
         */
    }
}
