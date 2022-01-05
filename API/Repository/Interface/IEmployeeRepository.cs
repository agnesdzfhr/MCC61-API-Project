using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MCC61_API_Project.Models;

namespace MCC61_API_Project.Repository.Interface
{
    interface IEmployeeRepository
    {
        IEnumerable<Employee> Get();
        Employee Get(string NIK);
        int Insert(Employee employee);
        int Update(string NIK, Employee employee);
        int Delete(string NIK);
    }
}
