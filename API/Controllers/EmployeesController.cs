using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MCC61_API_Project.Context;
using MCC61_API_Project.Models;
using MCC61_API_Project.Repository.Data;
using MCC61_API_Project.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

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

        //[Authorize(Roles = "Director, Manager")]
        [HttpGet]
        [Route("GetRegisterData")]
        public ActionResult GetRegisterData()
        {
            try
            {
                var getRegisterData = employeeRepository.GetRegisteredData();
                if (getRegisterData != null)
                {
                    //return Ok(getRegisterData);
                    return Ok(new { status = HttpStatusCode.OK, result = getRegisterData, message = "Update Success" });
                }
                return Ok(new { status = HttpStatusCode.OK, result = getRegisterData, message = "Update Failed" });
                //return Ok(getRegisterData);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("DeleteRegisterData/{NIK}")]
        public virtual ActionResult DeleteRegisterData(string NIK)
        {
            employeeRepository.DeleteEducation(NIK);
            var result = employeeRepository.DeleteRegisterData(NIK);
            return Ok(result);
        }

        //[HttpGet]
        //[Route("ChartUniversity")]
        //public ActionResult ChartUniversity()
        //{
        //    try
        //    {
        //        var getRegisterData = employeeRepository.ChartUniversity();
        //        if (getRegisterData != null)
        //        {
        //            return Ok(new { status = HttpStatusCode.OK, data = getRegisterData, message = "Data Found" });
        //        }
        //        return Ok(new { status = HttpStatusCode.BadRequest, message = "Data Not Found" });
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        [HttpGet]
        [Route("GetRegisterData/{nik}")]
        public ActionResult<Object> GetRegisterData(string NIK)
        {
            try
            {
                var getRegisterData = employeeRepository.GetRegisteredData(NIK);
                if (getRegisterData != null)
                {
                    //return Ok(new{ message="Data Found",data=getRegisterData});
                    return getRegisterData;
                }
                return getRegisterData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("Register")]
        public ActionResult Register(RegisterVM registerVM)
        {
            try
            {
                var register = employeeRepository.Register(registerVM);
                switch (register)
                {
                    case 1:
                        return Ok(new { status = HttpStatusCode.OK, result = register, message = "Register Success" });
                        //return Ok("Register Success");
                    case 2:
                        return Ok(new { status = HttpStatusCode.Conflict, result = register, message = "Register Failed, Phone Already Used" });
                        //return Ok("Register Failed, Phone Already Used");
                    case 3:
                        return Ok(new { status = HttpStatusCode.Conflict, result = register, message = "Register Failed, Email Already Used" });
                        //return Ok("Register Failed, Email Already Used");
                    default:
                        return Ok(new { status = HttpStatusCode.BadRequest, result = register, message = "Register Failed, Error Unknown" });
                        //return Ok("Register Failed");
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("UpdateRegister")]
        public ActionResult UpdateRegister(RegisterVM registerVM)
        {
            try
            {
                var register = employeeRepository.UpdateRegister(registerVM);
                switch (register)
                {
                    case 1:
                        return Ok(new { status = HttpStatusCode.OK, result = register, message = "Update Success" });
                    case 2:
                        return Ok(new { status = HttpStatusCode.Conflict, result = register, message = "Update Failed, Phone Already Used" });
                    case 3:
                        return Ok(new { status = HttpStatusCode.Conflict, result = register, message = "Update Failed, Email Already Used" });
                    default:
                        return Ok(new { status = HttpStatusCode.BadRequest, result = register, message = "Update Failed, NIK Not Found" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("TestCORS")]
        public ActionResult TestCORS()
        {
            return Ok("Test CORS Success");
        }
    }
}
