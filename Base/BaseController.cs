using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MCC61_API_Project.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MCC61_API_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;
        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public virtual ActionResult<Entity> Get()
        {
            var result = repository.Get();
            return Ok(result);
        }

        [HttpGet("{key}")]
        public virtual ActionResult<Entity> Get(Key key)
        {
            var result = repository.Get(key);
            return Ok(result);
        }

        [HttpPost]
        public virtual ActionResult<Entity> Insert(Entity entity)
        {
            var result = repository.Insert(entity);
            return Ok(result);
        }

        [HttpPut]
        public virtual ActionResult<Entity> Update(Entity entity)
        {
            var result = repository.Update(entity);
            return Ok(result);
        }

        [HttpDelete("{key}")]
        public virtual ActionResult<Entity> Delete(Key key)
        {
            var result = repository.Delete(key);
            return Ok(result);
        }
    }
}
