using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MCC61_API_Project.Context;
using MCC61_API_Project.Models;
using MCC61_API_Project.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace MCC61_API_Project.Repository
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext myContext; //koneksi dengan database
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }


        public override int Insert(Employee employee) //EmployeesRpository.cs
        {
            var checkdataNIK = myContext.Employees.Find(employee.NIK);
            var checkdataPhone = myContext.Employees.Where(e =>
            e.Phone == employee.Phone).SingleOrDefault();
            var checkdataEmail = myContext.Employees.Where(e =>
            e.Email == employee.Email).SingleOrDefault();
            if (checkdataNIK != null)
            {
                return 2;
            }
            else if (checkdataPhone != null)
            {
                return 3;
            }
            else if (checkdataEmail != null)
            {
                return 4;
            }
            else
            {
                var empCount = this.Get().Count() + 1;
                var year = DateTime.Now.Year;
                myContext.Employees.Add(employee);
                int respond = myContext.SaveChanges();
                return respond;
            }
        }

        public override int Update(string NIK, Employee employee) // EmployeesRepository.cs
        {
            var checkData = myContext.Employees.AsNoTracking().Where(e => e.NIK == NIK).FirstOrDefault();
            if (checkData != null)
            {
                myContext.Entry(checkData).State = EntityState.Detached;
                employee.NIK = NIK;
                var checkDataString = checkData.FirstName + checkData.LastName + checkData.Phone + checkData.BirthDate + checkData.Salary + checkData.Email + checkData.Gender;
                var employeeString = employee.FirstName + employee.LastName + employee.Phone + employee.BirthDate + employee.Salary + employee.Email + employee.Gender;
                bool isTrue = checkDataString != employeeString;
                if (isTrue == true)
                {
                    if (checkData.Phone != employee.Phone)
                    {
                        if (myContext.Employees.Where(e => e.Phone == employee.Phone).FirstOrDefault() != null)
                        {
                            return 3;
                        }
                        else
                        {
                            ModifyUpdate(employee);
                            return 1;
                        }
                    }
                    else if (checkData.Email != employee.Email)
                    {
                        if (myContext.Employees.Where(e => e.Email == employee.Email).FirstOrDefault() != null)
                        {
                            return 4;
                        }
                        else
                        {
                            ModifyUpdate(employee);
                            return 1;
                        }
                    }
                    else
                    {
                        ModifyUpdate(employee);
                        return 1;
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 2;
            }

        }

        private void ModifyUpdate(Employee employee)
        {
            myContext.Entry(employee).State = EntityState.Modified;
            myContext.SaveChanges();
        }
    }
}
