using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCC61_API_Project.ViewModels
{
    public class RegisterVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Phone { get; set; }

        public DateTime BirthDate { get; set; }

        public int Salary { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string Password { get; set; }
        public string Degree { get; set; }
        public float GPA { get; set; }
        public int EducationID { get; set; }
        public int UniversityID { get; set; }

    }

    public enum Gender
    {
        Male,
        Female
    }
}
