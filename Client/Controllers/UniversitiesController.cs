using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Base;
using Client.Repositories.Data;
using MCC61_API_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class UniversitiesController : BaseController<University, UniversityRepository, int>
    {
        public UniversitiesController(UniversityRepository repository) : base(repository)
        {

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
