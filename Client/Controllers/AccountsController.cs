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
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository repository;
        public AccountsController(AccountRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost("Accounts/Login")]
        //public JsonResult Login(LoginVM loginVM)
        //{
        //    //var result = repository.Login(loginVM);
        //    //return Json(result);
        //    var jwtToken = repository.Auth(loginVM).Result;
        //    var token = jwtToken.idtoken;

        //    //if (token == null)
        //    //{
        //    //    return RedirectToAction("index");
        //    //}

        //    HttpContext.Session.SetString("JWToken", token);
        //    //return RedirectToAction("index", "dashboard");
        //    return Json(token);
        //}

        //[HttpPost("Accounts/Login")]
        //public JsonResult Login(LoginVM loginVM)
        //{
        //    var result = repository.Login(loginVM);
        //    return Json(result);
        //}

        [HttpPost("Accounts/Login")]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            var jwtToken = await repository.Auth(loginVM);
            var token = jwtToken.idtoken;

            if (token == null)
            {
                //return RedirectToAction("index");
                TempData["message"] = jwtToken.message;
                return RedirectToAction("Login", "Employees"); //untuk return ke halaman Employees/Login.cshtml

            }

            HttpContext.Session.SetString("JWToken", token);

            return RedirectToAction("index", "Employees");
        }

        [Authorize]
        [HttpGet("Accounts/Logout/")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Auth(LoginVM login)
        {
            var jwtToken = await repository.Auth(login);
            var token = jwtToken.idtoken;

            if (token == null)
            {
                return RedirectToAction("index");
            }

            HttpContext.Session.SetString("JWToken", token);

            return RedirectToAction("index", "dashboard");
        }
    }
}
