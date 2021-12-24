using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
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


        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Gender Gender { get; set; }
        public string Password { get; set; }
        public string Degree { get; set; }
        public float GPA { get; set; }
        public int EducationID { get; set; }
        public int UniversityID { get; set; }

        public static string GetGender(int gender)
        {
            switch (gender)
            {
                case (int)Gender.Male:
                    return "Male";
                case (int)Gender.Female:
                    return "Female";
                default:
                    return "Invalid Data For Gender";
            }
        }

    }

    public enum Gender
    {
        Male,
        Female
    }

}
