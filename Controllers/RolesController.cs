using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MCC61_API_Project.Models;
using MCC61_API_Project.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MCC61_API_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : BaseController<Role, RoleRepository, int>
    {
        private readonly RoleRepository roleRepository;
        public RolesController(RoleRepository roleRepository) : base(roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        [Authorize(Roles = "Director")]
        [HttpPost]
        [Route("SignManager")]
        public ActionResult SignManager(AccountRole accountRole)
        {
            try
            {
                var result = roleRepository.SignManager(accountRole);
                switch (result)
                {
                    case 1:
                        return Ok(new { status = HttpStatusCode.OK, result = result, message = $"Account with NIK {accountRole.NIK} assigned as manager" });
                    default:
                        return Ok(new { status = HttpStatusCode.NotFound, result = result, message = $"Account with NIK {accountRole.NIK} not found in database" });

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
