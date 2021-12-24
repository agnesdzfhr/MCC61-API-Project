using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MCC61_API_Project.Models
{
    [Table("tb_m_employee")]
    public class Employee
    {
        [Key]
        [Range(10000, 99999, ErrorMessage = "NIK Must be 5 digit")]
        public string NIK { get; set; }

        [Required(ErrorMessage = "First Name is Required")]
        [StringLength(20, ErrorMessage = "Can't be longer than 20 character")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        [StringLength(20, ErrorMessage = "Can't be longer than 20 character")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone Number is Required")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "BirthDate is Required")]
        [DataType(DataType.DateTime, ErrorMessage = "Format Must Be YYYY-MM-DDT00:00:00")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Salary is Required")]
        [Range(100, 999999, ErrorMessage = "Salary Must in Between 100-999999")]
        public int Salary { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Gender is Required")]
        [Range(0, 1, ErrorMessage = "Must be 0 for Male or 1 for Female")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Gender Gender { get; set; }


        //untuk relasi ke account
        [JsonIgnore]
        public virtual Account Account { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
