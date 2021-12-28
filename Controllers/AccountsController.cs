using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly MyContext context;
        private readonly AccountRepository accountRepository;
        public AccountsController(AccountRepository accountRepository, MyContext myContext) : base(accountRepository)
        {
            this.accountRepository = accountRepository;
            this.context = myContext;
        }


        [HttpPost]
        [Route("Login")]
        public ActionResult Login(LoginVM loginVM)
        {
            try
            {
                var login = accountRepository.Login(loginVM);

                switch (login)
                {
                    case 1:
                        return Ok(new { status = HttpStatusCode.OK, login = login, message = "Login Success" });
                    case 2:
                        return Ok(new { status = HttpStatusCode.BadRequest, login = login, message = "Wrong Password" });
                    case 3:
                        return Ok(new { status = HttpStatusCode.NotFound, login = login, message = "Email Not Found" });
                    default:
                        return Ok(new { status = HttpStatusCode.BadRequest, login = login, message = "Login Failed" });
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("ForgotPassword")]
        public ActionResult ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            try
            {
                var forgotPassword = accountRepository.ForgotPassword(forgotPasswordVM);
                switch (forgotPassword)
                {
                    case 1:
                        return Ok(new { status = HttpStatusCode.OK, result = forgotPassword, message = "Check your email for the OTP" });
                    case 2: 
                        return Ok(new { status = HttpStatusCode.NotFound, result = forgotPassword, message = "Your email was not found in the database" });
                    default:
                        return Ok(new { status = HttpStatusCode.BadRequest, result = forgotPassword, message = "Failed to send OTP to your email" });

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("ChangePassword")]
        public ActionResult ChangePassword(ChangePasswordVM changePasswordVM)
        {
            try
            {
                var changePassword = accountRepository.ChangePassword(changePasswordVM);
                switch (changePassword)
                {
                    case 0:
                        return Ok(new { status = HttpStatusCode.NotAcceptable, result = changePassword, message = "OTP Expired, Please send request forgot password again!" });
                    case 1:
                        return Ok(new { status = HttpStatusCode.OK, result = changePassword, message = "Your Password Was Changed" });
                    case 2:
                        return Ok(new { status = HttpStatusCode.NotFound, result = changePassword, message = "Your email was not found in the database" });
                    case 3:
                        return Ok(new { status = HttpStatusCode.NotAcceptable, result = changePassword, message = "OTP has been used" });
                    default:
                        return Ok(new { status = HttpStatusCode.BadRequest, result = changePassword, message = "Wrong OTP" });

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
