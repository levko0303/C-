using Lab5.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using System.ServiceModel.Channels;

namespace Lab5.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ContractController : ControllerBase
    {
        public AppDb Db { get; }
        public ContractController(AppDb db)
        { Db = db; }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ContractParameters parameters)
        {
            int user_id = int.Parse(User.Identity.Name);
            await Db.Connection.OpenAsync();
            var query = new ContractQuery(Db);
            var result = await query.AllAsync(parameters, user_id);
            return new OkObjectResult(result);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contract>> GetOne(int id)
        {
            int user_id  = int.Parse(User.Identity.Name);
            await Db.Connection.OpenAsync();
            var query = new ContractQuery(Db);
            var result = await query.FindOneAsync(id, user_id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<Contract>> Post(Contract contract)
        {
            if (ModelState.IsValid)
            {
                contract.user_id = int.Parse(User.Identity.Name);
                await Db.Connection.OpenAsync();
                var query = new ContractQuery(Db);
                var result = await query.FindOneAsync(contract.id, contract.user_id);
                if (result != null)
                {
                    Response.StatusCode = 403;
                    return new JsonResult("Contract with such id already exists. Change id and try again");
                }
                await query.InsertAsync(contract);
                return new OkObjectResult(new Dictionary<string, dynamic> { { "Contract has been successfully created", contract } });
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Contract contract, int id)
        {
            if (ModelState.IsValid)
            {
                contract.user_id = int.Parse(User.Identity.Name);
                await Db.Connection.OpenAsync();
                var query = new ContractQuery(Db);
                var result = await query.FindOneAsync(id, contract.user_id);
                if (result is null)
                    return new NotFoundResult();
                await query.UpdateAsync(contract, id);
                return new OkObjectResult(new Dictionary<string, dynamic> { { "Contract has been successfully updated", contract } });
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            int user_id = int.Parse(User.Identity.Name);
            await Db.Connection.OpenAsync();
            var query = new ContractQuery(Db);
            var result = await query.FindOneAsync(id, user_id);
            if (result is null)
                return new NotFoundResult();
            await query.DeleteAsync(id, user_id);
            return new OkObjectResult(new Dictionary<string, dynamic> { { "Contract has been successfully deleted with id", id } }); 

        }

    }
}
