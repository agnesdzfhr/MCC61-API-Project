using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MCC61_API_Project.Models;
using MCC61_API_Project.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MCC61_API_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilingsController : BaseController<Profiling, ProfilingRepository, string>
    {
        private readonly ProfilingRepository profilingRepository;
        public ProfilingsController(ProfilingRepository profilingRepository) : base(profilingRepository)
        {
            this.profilingRepository = profilingRepository;
        }
    }
}
