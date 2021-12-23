using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MCC61_API_Project.Context;
using MCC61_API_Project.Models;
using MCC61_API_Project.Repository.Data;
using MCC61_API_Project.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MCC61_API_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly MyContext myContext;
        private readonly EmployeeRepository employeeRepository;
        public EmployeesController(EmployeeRepository employeeRepository, MyContext myContext) : base(employeeRepository)
        {
            this.employeeRepository = employeeRepository;
            this.myContext = myContext;
        }

        [HttpPost]
        [Route("Register")]
        public ActionResult Register(RegisterVM registerVM)
        {
            try
            {
                var register = employeeRepository.Register(registerVM);
                if (register == 1)
                {
                    return Ok(register);
                }
                return Ok(new { message = "Failed" });
            }
            catch(Exception e)
            {
                throw;
            }
        }
    }
}
