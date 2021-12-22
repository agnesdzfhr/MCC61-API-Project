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
        //Untuk mendapatkan semua data == select * from table
        //bisa juga pake IList --> (jika manggil, insert, update list terlebih dahulu)
        //IList bisa manipulasi data, kl IEnumerable cuma buat baca data
        Employee Get(string NIK);
        int Insert(Employee employee);
        int Update(string NIK, Employee employee);
        int Delete(string NIK);
    }
}
