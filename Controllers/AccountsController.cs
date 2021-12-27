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
    }
}
