using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MCC61_API_Project.Context;
using MCC61_API_Project.Models;
using MCC61_API_Project.ViewModels;

namespace MCC61_API_Project.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext context;
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.context = myContext;
        }

        public int Register(RegisterVM registerVM)
        {
            var year = DateTime.Now.Year;
            var empCount = context.Employees.ToList().Count + 1;
            var employee = new Employee
            {
                NIK = year+ '0' + empCount.ToString(),
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                Phone = registerVM.Phone,
                BirthDate = registerVM.BirthDate,
                Salary = registerVM.Salary,
                Email = registerVM.Email,
                Gender = (Models.Gender)registerVM.Gender
            };
            context.Employees.Add(employee);
            context.SaveChanges();
            var ac = new Account
            {
                NIK = employee.NIK,
                Password = registerVM.Password
            };
            context.Accounts.Add(ac);
            context.SaveChanges();
            var ed = new Education
            {
                Degree = registerVM.Degree,
                GPA = registerVM.GPA,
                UniversityID = registerVM.UniversityID
            };
            context.Educations.Add(ed);
            context.SaveChanges();
            var pr = new Profiling
            {
                NIK = employee.NIK,
                EducationId = ed.EducationID
            };
            context.Profilings.Add(pr);

            context.SaveChanges();
            return 1;
        }
    }
}
