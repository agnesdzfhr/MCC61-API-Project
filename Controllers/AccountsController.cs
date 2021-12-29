using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MCC61_API_Project.Context;
using MCC61_API_Project.Models;
using MCC61_API_Project.Repository.Data;
using MCC61_API_Project.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MCC61_API_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly MyContext context;
        private readonly AccountRepository accountRepository;
        public IConfiguration _configuration;
        public AccountsController(AccountRepository accountRepository, MyContext myContext, IConfiguration configuration) : base(accountRepository)
        {
            this.accountRepository = accountRepository;
            this.context = myContext;
            this._configuration = configuration;
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
                        var getRoles = accountRepository.GetRoles(loginVM.Email);

                        var claims = new List<Claim>
                        {
                            new Claim("email", loginVM.Email)

                        };
                        foreach (var roleData in getRoles)
                        {
                            claims.Add(new Claim("roles", roleData.ToString()));
                        }

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                                _configuration["Jwt:Issuer"],
                                _configuration["Jwt:Audience"],
                                claims,
                                expires: DateTime.UtcNow.AddMinutes(10),
                                signingCredentials: signIn
                            );
                        var idToken = new JwtSecurityTokenHandler().WriteToken(token);
                        claims.Add(new Claim("TokenSecurity", idToken.ToString()));
                        return Ok(new { status = HttpStatusCode.OK, idtoken = idToken, message = "Login Success" });
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
