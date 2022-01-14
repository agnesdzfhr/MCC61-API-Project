using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MCC61_API_Project.Models;

namespace MCC61_API_Project.ViewModels
{
    public class RegisterVM
    {
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Phone { get; set; }

        public DateTime BirthDate { get; set; }

        public string BirthDateStr { get; set; }

        public int Salary { get; set; }
        public string Email { get; set; }


        public Models.Gender Gender { get; set; }
        public string Password { get; set; }
        public string Degree { get; set; }
        public float GPA { get; set; }
        public int EducationID { get; set; }
        public int UniversityID { get; set; }

        public string UniversityName { get; set; }

        public List<string> Role { get; set; }

        //public List<Education> Educations { get; set; }

        public static string GetGender(int gender)
        {
            switch (gender)
            {
                case (int)Models.Gender.Male:
                    return "Male";
                case (int)Models.Gender.Female:
                    return "Female";
                default:
                    return "Invalid Data For Gender";
            }
        }

    }

}
