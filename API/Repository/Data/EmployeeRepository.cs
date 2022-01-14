using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MCC61_API_Project.Context;
using MCC61_API_Project.Models;
using MCC61_API_Project.ViewModels;
using Microsoft.EntityFrameworkCore;

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
            var checkPhone = context.Employees.Where(p => p.Phone == registerVM.Phone).FirstOrDefault();
            var checkEmail = context.Employees.Where(p => p.Email == registerVM.Email).FirstOrDefault();
            var increament = context.Employees.ToList().Count;
            var formattedNIK = "";
            if (increament == 0)
            {
                formattedNIK = DateTime.Now.Year.ToString() + "0" + (increament + 1).ToString();

            }
            else
            {
                var increament2 = context.Employees.ToList().Max(e => e.NIK);
                formattedNIK = (Int32.Parse(increament2) + 1).ToString();

            }
            if (checkPhone != null)
            {
                return 2;
            }
            else if (checkEmail != null)
            {
                return 3;
            }
            else
            {
                var employee = new Employee
                {
                    NIK = formattedNIK,
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
                    Password = Hashing.HashPassword(registerVM.Password)
                };
                context.Accounts.Add(ac);
                context.SaveChanges();
                var ar = new AccountRole
                {
                    NIK = ac.NIK,
                    RoleId = 3
                };
                context.AccountRoles.Add(ar);
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

        public int UpdateRegister(RegisterVM registerVM)
        {
            var findNIK = context.Employees.AsNoTracking().Where(e => e.NIK == registerVM.NIK).FirstOrDefault();
            context.Entry(findNIK).State = EntityState.Detached;

            var employee = new Employee
            {
                NIK = registerVM.NIK,
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                Phone = registerVM.Phone,
                BirthDate = registerVM.BirthDate,
                Salary = registerVM.Salary,
                Email = registerVM.Email,
                Gender = (Models.Gender)registerVM.Gender
            };
            context.Entry(employee).State = EntityState.Modified;
            context.SaveChanges();
            var findEduID = context.Profilings.Find(registerVM.NIK);
            var ed = new Education
            {
                EducationID = findEduID.EducationId,
                Degree = registerVM.Degree,
                GPA = registerVM.GPA,
                UniversityID = registerVM.UniversityID
            };
            context.Entry(ed).State = EntityState.Modified;
            context.SaveChanges();
            return 1;
        }

        public int CheckEmail(Employee employee)
        {
            var checkEmail = context.Employees.Where(e => e.Email == employee.Email).FirstOrDefault();
            if (checkEmail != null)
                return 1;
            else
                return 0;
        }

        public int CheckPhone(Employee employee)
        {
            var checkPhone = context.Employees.Where(e => e.Phone == employee.Phone).FirstOrDefault();
            if (checkPhone != null)
                return 1;
            else
                return 0;
        }

        public IEnumerable<Object> GetRegisteredData()
        {
            var grd = from e in context.Employees
                      join ac in context.Accounts
                         on e.NIK equals ac.NIK
                      join pr in context.Profilings
                         on ac.NIK equals pr.NIK
                      join ed in context.Educations
                        on pr.EducationId equals ed.EducationID
                      join u in context.Universities
                         on ed.UniversityID equals u.UniversityID
                      //join ar in context.AccountRoles
                      //  on e.NIK equals ar.NIK
                      //join r in context.Roles
                      //on ar.RoleId equals r.RoleId

                      select new
                      {
                          NIK = e.NIK,
                          FirstName = e.FirstName,
                          LastName = e.LastName,
                          Phone = e.Phone,
                          Email = e.Email,
                          BirthDate = e.BirthDate,
                          BirthDateStr = e.BirthDate.ToString("dddd, dd MMMM yyyy"),
                          Salary = e.Salary,
                          Gender = RegisterVM.GetGender((int)e.Gender),
                          GPA = ed.GPA,
                          Degree = ed.Degree,
                          UniversityId = u.UniversityID,
                          UniversityName = u.Name,
                          Role = context.AccountRoles.Where(ar => ar.NIK == e.NIK).Select(ar => ar.Role.RoleName).ToList()
                          //Educations = context.Profilings.Where(p => p.EducationId == ed.EducationID).Select(p=> p.Education).ToList()
                      };
            return grd;
        }

        public RegisterVM GetRegisteredData(string NIK)
        {
            //    var grd = from e in context.Employees
            //              join ac in context.Accounts
            //                 on e.NIK equals ac.NIK
            //              join pr in context.Profilings
            //                 on ac.NIK equals pr.NIK
            //              join ed in context.Educations
            //                on pr.EducationId equals ed.EducationID
            //              join u in context.Universities
            //                 on ed.UniversityID equals u.UniversityID
            //              where e.NIK == NIK

            var query = context.Employees.Where(e => e.NIK == NIK)
                                        .Include(e => e.Account)
                                        .ThenInclude(a => a.Profiling)
                                        .ThenInclude(p => p.Education)
                                        .ThenInclude(ed => ed.University)
                                        .FirstOrDefault();
            if(query == null)
            {
                return null;
            }

            var grd = new RegisterVM
            {
                NIK = query.NIK,
                FirstName = query.FirstName,
                LastName = query.LastName,
                Phone = query.Phone,
                Email = query.Email,
                BirthDate = query.BirthDate,
                BirthDateStr = query.BirthDate.ToString("yyyy-MM-dd"),
                Salary = query.Salary,
                //Gender = RegisterVM.GetGender((int)e.Gender),
                Gender = query.Gender,
                GPA = query.Account.Profiling.Education.GPA,
                Degree = query.Account.Profiling.Education.Degree,
                UniversityID = query.Account.Profiling.Education.University.UniversityID,
                UniversityName = query.Account.Profiling.Education.University.Name,
                Role = context.AccountRoles.Where(ar => ar.NIK == query.NIK).Select(ar => ar.Role.RoleName).ToList()
            };

            return grd;
        }

        //public IEnumerable<Object> ChartUniversity()
        //{
        //    var list = from uni in context.Universities 
        //               join edu in context.Educations
        //               on uni.UniversityID equals edu.UniversityID into joined
        //               from j in joined.DefaultIfEmpty()
        //               select new
        //               {
        //                   //Education = j,
        //                   //University = uni

        //                   //UniversityId = j.Key.UniversityID,
        //                   //UniversityName = Group.Key.Name,
        //                   //BaseUniversity = Group.Count()

        //                   UniversityId = Group.,
        //                   //UniversityName = Group.Key.Name,
        //                   BaseUniversity = j.grou
        //               };
        //    return list.ToList();
        //}

        public class Hashing
        {
            private static string GetRandomSalt()
            {
                return BCrypt.Net.BCrypt.GenerateSalt(12);
            }
            public static string HashPassword(string Password)
            {
                return BCrypt.Net.BCrypt.HashPassword(Password, GetRandomSalt());
            }
            public static bool ValidatePassword(string Password, string correctHash)
            {
                return BCrypt.Net.BCrypt.Verify(Password, correctHash);
            }
        }

        public int DeleteRegisterData(string NIK)
        {
            var emp = context.Employees.Find(NIK);
            if (emp == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.Employees.Remove(emp);
            context.SaveChanges();
            return 1;
        }

        public int DeleteEducation(string NIK)
        {
            var profiling = context.Profilings.Find(NIK);
            var edu = context.Educations.Where(edu => edu.EducationID == profiling.EducationId).FirstOrDefault();
            if (edu == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.Educations.Remove(edu);
            context.SaveChanges();
            return 1;

        }
    }
}
