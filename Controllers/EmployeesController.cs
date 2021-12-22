using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MCC61_API_Project.Models;
using MCC61_API_Project.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MCC61_API_Project.Controllers
{
    [Route("api/[controller]")] //nama urlnya nanti localhost:[port]/api/[namacontroller]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeRepository employeeRepository;
        public EmployeesController(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpPost]
        public ActionResult Insert(Employee employee)
        {
            try
            {
                var insert = employeeRepository.Insert(employee);

                switch (insert)
                {
                    case 1:
                        return Ok(new { status = HttpStatusCode.OK, insert, message = "Input Data Success" });               
                    case 2:
                        return Ok(new { status = HttpStatusCode.Conflict, message = "NIK Already Used" });
                    case 3:
                        return Ok(new { status = HttpStatusCode.Conflict, message = "Phone Already Used" });
                    case 4:
                        return Ok(new { status = HttpStatusCode.Conflict, message = "Email Already Used" });
                    default:
                        return Ok(new { status = HttpStatusCode.Conflict, message = "Input Data Failed" });
                }
            }
            catch (Exception e)
            {
                return Ok(new { status = HttpStatusCode.BadRequest, message = e.Message });
            }
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var data = employeeRepository.Get();
                if (data.Count() > 0)
                {
                    return Ok(new { status = HttpStatusCode.OK, data, message = "Data Found" });
                }
                else
                {
                    return Ok(new { status = HttpStatusCode.NoContent, message = "Data Empty" });
                }
            }
            catch (Exception e)
            {
                return Ok(new { status = HttpStatusCode.BadRequest, message = e.Message });
            }
        }

        [HttpGet("{NIK}")]
        public ActionResult Get(string NIK)
        {
            try
            {
                var data = employeeRepository.Get(NIK);
                if (data != null)
                {
                    return Ok(new { status = HttpStatusCode.OK, data, message = "Data Found" });
                }
                else
                {
                    return Ok(new { status = HttpStatusCode.NotFound, message = "NIK Not Found" });
                }
            }
            catch (Exception e)
            {
                return Ok(new { status = HttpStatusCode.BadRequest, message = e.Message });
            }
        }

        [HttpDelete("{NIK}")]
        public ActionResult Delete(string NIK)
        {
            try
            {
                var delete = employeeRepository.Delete(NIK);
                return Ok(new { status = HttpStatusCode.OK, delete = delete, message = $"Successfully Deleted Data with NIK {NIK}" });
            }
            catch (Exception e)
            {
                return Ok(new { status = HttpStatusCode.BadRequest, message = e.Message });
            }
        }

        [HttpPut("{NIK}")] //EmployeeController.cs
        public ActionResult Update(string NIK, Employee employee)
        {
            try
            {
                var update = employeeRepository.Update(NIK, employee);

                switch (update)
                {
                    case 1:
                        return Ok(new { status = HttpStatusCode.OK, update, message = $"Succesfully Updated Data with NIK {NIK}" });
                    case 2:
                        return Ok(new { status = HttpStatusCode.Conflict, message = "NIK Not Found" });
                    case 3:
                        return Ok(new { status = HttpStatusCode.Conflict, message = "Phone Already Used" });
                    case 4:
                        return Ok(new { status = HttpStatusCode.Conflict, message = "Email Already Used" });
                    default:
                        return Ok(new { status = HttpStatusCode.Conflict, message = "No Data Change" });
                }

            }
            catch (Exception e)
            {
                return Ok(new { status = HttpStatusCode.BadRequest, message = e.Message });
            }
        }
    }
}
