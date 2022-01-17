using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Base;
using Client.Repositories.Data;
using MCC61_API_Project.Models;
using MCC61_API_Project.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    [Authorize]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository repository;
        public EmployeesController(EmployeeRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Chart()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            var token = HttpContext.Session.GetString("JWToken");

            if (token == null)
            {
                return View();
            }

            return RedirectToAction("index", "Employees");
            //    if (HttpContext.Session.IsAvailable)
            //    {
            //        return RedirectToAction("index", "Employees");
            //    }
            //return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetRegisterData()
        {
            var result = await repository.GetRegisterData();
            return Json(result);
        }

        [HttpGet("Employees/GetRegisterByNIK/{NIK}")]
        public async Task<JsonResult> GetRegisterByNIK(string NIK)
        {
            var result = await repository.GetRegisterByNIK(NIK);
            return Json(result);
        }

        [HttpPost("Employees/Register")]
        public JsonResult Register(RegisterVM registerVM)
        {
            var result = repository.Register(registerVM);
            return Json(result);
        }

        [HttpPut("Employees/UpdateRegister")]
        public JsonResult UpdateRegister(RegisterVM registerVM)
        {
            var result = repository.UpdateRegister(registerVM);
            return Json(result);
        }

        [HttpDelete("Employees/DeleteRegisterData/{NIK}")]
        public JsonResult DeleteRegisterData(string NIK)
        {
            var result = repository.Delete(NIK);
            return Json(result);
        }
    }

}
